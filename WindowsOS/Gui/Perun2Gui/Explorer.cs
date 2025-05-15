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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Perun2Gui
{
    internal static class Explorer
    {
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);
        
        
        public static void SelectFiles(List<string> files)
        {
            if (files.Count == 0)
            {
                return;
            }

            var groups = files.GroupBy(e => Directory.GetParent(e).FullName);

            foreach (var group in groups)
            {
                List<string> paths = group.ToList();
                string address = Directory.GetParent(paths.First()).FullName;
                SelectInDirectory(paths, address);
            }
        }

        private static void SelectInDirectory(List<string> selected, string address)
        {
            IntPtr nativeFolder;
            uint psfgaoOut;
            SHParseDisplayName(address, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);

            // location does not exists
            if (nativeFolder == IntPtr.Zero)
            {
                return;
            }

            // escape if there is nothing to select
            if (selected.Count == 0)
            {
                return;
            }

            // fill the array of files and directories
            IntPtr[] fileArray = new IntPtr[selected.Count];
            for (int j = 0; j < selected.Count; j++)
            {
                SHParseDisplayName(System.IO.Path.Combine(address, selected[j]), 
                    IntPtr.Zero, out fileArray[j], 0, out psfgaoOut);
            }

            SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

            // free memory
            Marshal.FreeCoTaskMem(nativeFolder);
            for (int j = 0; j < selected.Count; j++)
            {
                Marshal.FreeCoTaskMem(fileArray[j]);
            }
        }
    }
}
