using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Perun2Uninstaller.Actions
{
    class DeleteRegistryKeys : Action
    {
        private static readonly string DropdownMenu = "Directory\\Background\\shell\\Perun2";
        private static readonly string DropdownMenu2 = "Folder\\shell\\Perun2";
        private static readonly string HereMenu = "Directory\\Background\\shell\\Perun2Here";
        private static readonly string HereMenu2 = "Folder\\shell\\Perun2Here";
        private static readonly string GlobalScriptsRoot = "*\\Perun2";
        private static readonly string PerunFiles = "Perun2\\shell\\RunPerun2Now";
        private static readonly string UninstallInfo = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Perun2";
        private static readonly string UninstallInfo32on64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Perun2";


        public override bool Do()
        {
            RemoveKeyFromClasses(DropdownMenu);
            RemoveKeyFromClasses(DropdownMenu2);
            RemoveKeyFromClasses(HereMenu);
            RemoveKeyFromClasses(HereMenu2);
            RemoveKeyFromClasses(GlobalScriptsRoot);
            RemoveKeyFromClasses(PerunFiles);
            RemoveKeyFromLocalMachine(UninstallInfo);
            RemoveKeyFromLocalMachine(UninstallInfo32on64);
            return true;
        }

        private void RemoveKeyFromClasses(string path)
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

        private void RemoveKeyFromLocalMachine(string path)
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
    }
}
