import mmap
import struct
import sys
import os
import ctypes
import time
import importlib.util
from ctypes import wintypes


SCRIPT_PATH = sys.argv[1]
MEMORY_ID = sys.argv[2]

SHM_NAME_HEAD = "Local\\SharedMemoryPerunPython"
SHM_NAME = SHM_NAME_HEAD + str(MEMORY_ID)

STATUS_PERUN_NULL = 0
STATUS_PERUN_IDLE = 1
STATUS_PERUN_ASKS = 2

STATUS_PYTHON_NULL = 0
STATUS_PYTHON_IDLE = 1
STATUS_PYTHON_WORKING = 2
STATUS_PYTHON_RESPONDED = 3
STATUS_PYTHON_ERROR = 4

STATUS_LOCATION_STILL_THE_SAME = 0
STATUS_LOCATION_CHANGED = 1

INT_SIZE = 4
CHAR_SIZE = 2
STRING_LENGTH = 33000

OFFSET_PERUN_STATUS =          INT_SIZE * 0
OFFSET_PYTHON_STATUS =         INT_SIZE * 1
OFFSET_RESULT =                INT_SIZE * 2
OFFSET_LOCATION_STATUS =       INT_SIZE * 3
OFFSET_LENGTH_FILE_NAME =      INT_SIZE * 4
OFFSET_LENGTH_LOCATION_PATH =  INT_SIZE * 5
OFFSET_FILE_NAME =             INT_SIZE * 6
OFFSET_LOCATION_PATH =         INT_SIZE * 6 + (STRING_LENGTH * CHAR_SIZE)

AMOUNT_INTS = 7
AMOUNT_STRINGS = 2

TOTAL_SIZE = AMOUNT_INTS * INT_SIZE + AMOUNT_STRINGS * STRING_LENGTH * CHAR_SIZE

# Shared memory description.
# Byte 1-4:            Perun2 state
# Byte 5-8:            Python3 state
# Byte 9-12:           result
# Byte 13-16:          location state
# Byte 17-20:          length of file path
# Byte 21-24:          length of location path
# Byte 25-33024:       file path
# Byte 33025-66024:    location path


def load_function(file_path, function_name):
    module_name = os.path.splitext(os.path.basename(file_path))[0]

    spec = importlib.util.spec_from_file_location(module_name, file_path)
    if spec is None:
        raise ImportError(f"Could not load spec from {file_path}")

    module = importlib.util.module_from_spec(spec)

    spec.loader.exec_module(module)

    if not hasattr(module, function_name):
        raise ImportError(f"Function {function_name} not found in {file_path}")
    
    return getattr(module, function_name)


MAIN_FUNCTION = load_function(SCRIPT_PATH, "main")


def get_result(file_name):
    try:
        boolean = MAIN_FUNCTION(file_name)
    
        if isinstance(boolean, bool):
            return 1 if boolean else 0
 
    except Exception as e:
        print(f"Uncaught Python3 exception: {e}.")

    return 0
    
    
with mmap.mmap(-1, TOTAL_SIZE, SHM_NAME, access=mmap.ACCESS_WRITE) as mem:
    def fill_with_zeros():
        BLOCK_SIZE = 1024
        for offset in range(0, TOTAL_SIZE, BLOCK_SIZE):
            mem.seek(offset)
            mem.write(b'\x00' * min(BLOCK_SIZE, TOTAL_SIZE - offset))
    
    def read_int(offset):
        mem.seek(offset)
        return struct.unpack('<i', mem.read(INT_SIZE))[0]
        
    def write_int(offset, value):
        mem.seek(offset)
        mem.write(struct.pack('<i', value))
    
    def read_string(offset, length):
        if length == 0:
            return ""
        mem.seek(offset)
        wide_bytes = mem.read(length * CHAR_SIZE)
        return wide_bytes.decode('utf-16le')

    fill_with_zeros()
    write_int(OFFSET_PYTHON_STATUS, STATUS_PYTHON_IDLE);
    
    perun_status = 0
    file_name = ""
    file_name_length = 0
    location_status = 0
    location_path = ""
    location_path_length = 0
    result = False
    
    try:
        while True:
            perun_status = read_int(OFFSET_PERUN_STATUS)
        
            if perun_status == STATUS_PERUN_ASKS:
                write_int(OFFSET_PERUN_STATUS, STATUS_PERUN_IDLE)
            
                location_status = read_int(OFFSET_LOCATION_STATUS)
                if location_status == STATUS_LOCATION_CHANGED:
                    write_int(OFFSET_LOCATION_STATUS, STATUS_LOCATION_STILL_THE_SAME)
                    location_path_length = read_int(OFFSET_LENGTH_LOCATION_PATH)
                    location_path = read_string(OFFSET_LOCATION_PATH, location_path_length)
                    os.chdir(location_path)
            
                write_int(OFFSET_PYTHON_STATUS, STATUS_PYTHON_WORKING)
            
                file_name_length = read_int(OFFSET_LENGTH_FILE_NAME)
                file_name = read_string(OFFSET_FILE_NAME, file_name_length)
            
                result = get_result(file_name)
                write_int(OFFSET_RESULT, result)
                write_int(OFFSET_PYTHON_STATUS, STATUS_PYTHON_RESPONDED)
    
    except BaseException as be:
        write_int(OFFSET_PYTHON_STATUS, STATUS_PYTHON_ERROR)
        
        while True:
            time.sleep(0.1)
            # If crashed, the Perun2 part closes this program. Wait until it happens.
