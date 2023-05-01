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
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Perun2Uninstaller
{
    public static class EnvVariable
    {

        public static void AddToPath(string path)
        {
            string oldpath = UserPathGet();
            List<string> newpathlist = oldpath.Split(';').ToList();
            if (newpathlist.Contains(path))
            {
                return;
            }
            newpathlist.Add(path);

            string newpath = String.Join(";", newpathlist.ToArray());

            UserPathSet(newpath);
            UpdateEnvPath();
        }

        public static void DeleteFromPath(string path)
        {
            string oldpath = UserPathGet();
            List<string> newpathlist = oldpath.Split(';').ToList();

            if (!newpathlist.Contains(path))
            {
                return;
            }

            newpathlist.Remove(path);
            string newpath = String.Join(";", newpathlist.ToArray());

            UserPathSet(newpath);

            UpdateEnvPath();
        }

        public static string UserPathGet()
        {
            // Reads Registry Path "HKCU\Environment\Path"
            string subKey = "Environment";

            Microsoft.Win32.RegistryKey sk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKey);

            if (sk == null)
                return null;
            else
                return sk.GetValue("Path").ToString();
        }

        public static void UserPathSet(string newpath)
        {
            // Writes Registry Path "HKCU\Environment\Path"
            string subKey = "Environment";

            Microsoft.Win32.RegistryKey sk1 = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subKey);
            sk1.SetValue("Path", newpath);
        }


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessageTimeout(IntPtr hWnd,
                    uint Msg, UIntPtr wParam, string lParam,
                    SendMessageTimeoutFlags fuFlags,
                    uint uTimeout, out UIntPtr lpdwResult);

        private enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0, SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2, SMTO_NOTIMEOUTIFNOTHUNG = 0x8
        }

        private static void UpdateEnvPath()
        {
            // SEE: https://support.microsoft.com/en-us/help/104011/how-to-propagate-environment-variables-to-the-system
            // Need to send WM_SETTINGCHANGE Message to 
            //    propagage changes to Path env from registry
            IntPtr HWND_BROADCAST = (IntPtr)0xffff;
            const UInt32 WM_SETTINGCHANGE = 0x001A;
            UIntPtr result;
            IntPtr settingResult = SendMessageTimeout(HWND_BROADCAST,
                WM_SETTINGCHANGE, (UIntPtr)0,
                "Environment",
                SendMessageTimeoutFlags.SMTO_ABORTIFHUNG,
                1000, out result); // 5000
        }
    }
}
