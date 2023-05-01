/*
    This file is part of Perun2.
    Perun2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    Perun2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with Perun2. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FastColoredTextBoxNS
{
    public static class PlatformType
    {
        const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

        [StructLayout(LayoutKind.Sequential)]
        struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [DllImport("kernel32.dll")]
        static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        public static Platform GetOperationSystemPlatform()
        {
            var sysInfo = new SYSTEM_INFO();

            // WinXP and older - use GetNativeSystemInfo
            if (Environment.OSVersion.Version.Major > 5 ||
                (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1))
            {
                GetNativeSystemInfo(ref sysInfo);
            }
            // else use GetSystemInfo
            else
            {
                GetSystemInfo(ref sysInfo);
            }

            switch (sysInfo.wProcessorArchitecture)
            {
                case PROCESSOR_ARCHITECTURE_IA64:
                case PROCESSOR_ARCHITECTURE_AMD64:
                    return Platform.X64;

                case PROCESSOR_ARCHITECTURE_INTEL:
                    return Platform.X86;

                default:
                    return Platform.Unknown;
            }
        }
    }

    public enum Platform
    {
        X86,
        X64,
        Unknown
    }

}
