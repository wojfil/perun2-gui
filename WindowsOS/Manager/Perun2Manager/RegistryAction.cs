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
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Diagnostics;


namespace Perun2Manager
{
    static class RegistryAction
    {

        private static bool IsGlobalScriptKey(string name)
        {
            if (name.Length < Constants.KEYNAME_MINIMUM_LENGTH)
            {
                return false;
            }

            if (!name.StartsWith(Constants.KEYNAME_PREFIX) )
            {
                return false;
            }

            for (int i = 0; i < Constants.KEYNAME_NUMERATION_DIGITS; i++)
            {
                if (!Char.IsDigit(name[Constants.KEYNAME_PREFIX.Length + i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static List<string> GetRegistryKeys()
        {
            List<string> values = new List<string>();

            using (RegistryKey MyReg = Registry.ClassesRoot.OpenSubKey(Constants.REGISTRY_GLOBAL_SCRIPTS_ROOT))
            {
                var subKeys = MyReg.GetSubKeyNames();

                foreach (string s in subKeys)
                {
                    if (IsGlobalScriptKey(s))
                    {
                        values.Add(s);
                    }
                }
            }

            values.Sort();
            return values;
        }

        private static string GetKeyName(string name, int id)
        {
            return Constants.KEYNAME_PREFIX + id.ToString().PadLeft(Constants.KEYNAME_NUMERATION_DIGITS, '0') + name;
        }

        public static void ClearAndSetNewKeys(List<string> oldKeys, List<string> newNames)
        {
            foreach (var o in oldKeys)
            {
                DeleteItem(o);
            }

            int i = 0;
            foreach (var n in newNames)
            {
                AddItem(n, i);
                i++;
            }
        }


        public static void DeleteItem(string name)
        {
            string MenuName = Constants.REGISTRY_GLOBAL_SCRIPTS_ROOT + "\\" + name;
            string Command = MenuName + Constants.REGISTRY_COMMAND;

            try
            {
                using (RegistryKey reg = Registry.ClassesRoot.OpenSubKey(Command))
                {
                    Registry.ClassesRoot.DeleteSubKey(Command);
                }

                using (RegistryKey reg2 = Registry.ClassesRoot.OpenSubKey(MenuName))
                {
                    Registry.ClassesRoot.DeleteSubKey(MenuName);
                }
            }
            catch (Exception ex)
            {
                Popup.Error(ex.ToString());
            }
        }

        public static void AddItem(string name, int id)
        {
            string regName = GetKeyName(name, id);
            string MenuName = Constants.REGISTRY_GLOBAL_SCRIPTS_ROOT + "\\" + regName;
            string Command = MenuName + Constants.REGISTRY_COMMAND;

            try
            {
                using (RegistryKey reg = Registry.ClassesRoot.CreateSubKey(MenuName))
                {
                    reg.SetValue("", name);
                    reg.SetValue("MUIVerb", name);
                }

                string exe = Paths.GetInstance().GetExePath();
                string script = Paths.GetInstance().GetScriptPath(name);

                using (RegistryKey reg2 = Registry.ClassesRoot.CreateSubKey(Command))
                {
                    string c = "\"" + exe + "\" -s -o -d \"%V \" \"" + script + "\"";
                    reg2.SetValue("", c);
                }
            }
            catch (Exception ex)
            {
                Popup.Error(ex.ToString());
            }
        }
    }
}
