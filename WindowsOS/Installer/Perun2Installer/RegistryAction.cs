using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Diagnostics;

namespace Perun2Installer
{
    public static class RegistryAction
    {
        public static List<string> GetRegistryKeys(string path)
        {
            List<string> values = new List<string>();

            using (RegistryKey MyReg = Registry.ClassesRoot.OpenSubKey(path))
            {
                var subKeys = MyReg.GetSubKeyNames();

                foreach (string s in subKeys)
                {
                    values.Add(s);
                }
            }

            return values;
        }

        public static void AddKey(string path, List<Tuple<string, string>> values)
        {
            try
            {
                using (RegistryKey reg = Registry.ClassesRoot.CreateSubKey(path))
                {
                    foreach (var v in values)
                    {
                        reg.SetValue(v.Item1, v.Item2);
                    }
                }
            }
            catch (Exception) { }
        }

        public static void AddKeyToLocalMachine(string path, List<Tuple<string, string>> values)
        {
            try
            {
                using (RegistryKey reg = Registry.LocalMachine.CreateSubKey(path))
                {
                    foreach (var v in values)
                    {
                        reg.SetValue(v.Item1, v.Item2);
                    }
                }
            }
            catch (Exception) { }
        }

        public static void AddDwordValue(string path, string name, int value)
        {
            try
            {
                using (RegistryKey reg = Registry.ClassesRoot.OpenSubKey(path, true))
                {
                    reg.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
            catch (Exception) { }
        }

        public static void RemoveKey(string path)
        {
            try
            {
                using (RegistryKey reg = Registry.ClassesRoot.OpenSubKey(path))
                {
                    Registry.ClassesRoot.DeleteSubKeyTree(path);
                }
            }
            catch (Exception) { }
        }

        public static void RemoveKeyFromLocalMachine(string path)
        {
            try
            {
                using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(path))
                {
                    Registry.LocalMachine.DeleteSubKeyTree(path);
                }
            }
            catch (Exception) { }
        }

        public static void EnableLongPaths()
        {
            try
            {
                string path = "SYSTEM\\CurrentControlSet\\Control\\FileSystem";

                using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(path, true))
                {
                    reg.SetValue("LongPathsEnabled", 1, RegistryValueKind.DWord);
                }

            }
            catch (Exception e)
            {
                Popup.Error(e.Message);
            }
        }

        public static bool KeyExistsOnLocalMachine(string path)
        {
            bool result;

            try
            {
                using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(path, true))
                {
                    result = reg != null;
                }
            }
            catch (Exception) 
            {
                result = false;
            }

            return result;
        }

        public static string GetInstallationPath(string regPath)
        {
            try
            {
                using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(regPath, true))
                {
                    var v = reg.GetValue("InstallLocation").ToString().EscapeQuote();
                    if (v.Contains("\\"))
                    {
                        var v2 = v.Remove(v.LastIndexOf("\\"));
                        return v2;
                    }

                }
            }
            catch (Exception) { }

            return "";
        }

    }
}
