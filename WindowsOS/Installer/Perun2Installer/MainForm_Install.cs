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
using Perun2Installer.Properties;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using System.Security.Permissions;
using System.Diagnostics;


namespace Perun2Installer
{
    public partial class MainForm : Form
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);


        private void StartInstall()
        {
            nextButton.Enabled = false;
            backButton.Enabled = false;
            cancelButton.Enabled = false;

            try
            {
                bool result = installationActions.Run();
                if (result)
                {
                    EndInstallSuccess();
                }
                else
                {
                    Popup.Error("Installation failed.");
                    EndInstallFail();
                    return;
                }
            }
            catch (Exception e)
            {
                Popup.Error("Installation failed. Error: " + e.Message);
                installationActions.CleanUp();
                EndInstallFail();
                return;
            }
        }

        private void EndInstallSuccess()
        {
            PageManager.Next();

            nextButton.Enabled = true;
            nextButton.Text = "Finish";
            cancelButton.Enabled = false;
            backButton.Enabled = false;
        }

        private void EndInstallFail()
        {
            PageManager.Next();
            PageManager.Next();

            nextButton.Enabled = true;
            nextButton.Text = "Finish";
            cancelButton.Enabled = false;
            backButton.Enabled = false;

        }

        private void Finish()
        {
            if (desktopShortcutBox.Checked)
            {
                try
                {
                    CreateDesktopShortcut();
                }
                catch (Exception) { }
            }

            if (menuStartShortcutBox.Checked)
            {
                try
                {
                    CreateMenuStartShortcut();
                }
                catch (Exception) { }
            }

            if (openPerun2Box.Checked)
            {
                try
                {
                    OpenPerun2();
                }
                catch (Exception) { }
            }
            Application.Exit();
        }

        private void OpenPerun2()
        {
            Process.Start(Paths.GetInstance().GetGuiPath());
        }

        private void EnableNextButton()
        {
            nextButton.Enabled = true;
        }

        private void DisableNextButton()
        {
            nextButton.Enabled = false;
        }

        private void EnableBackButton()
        {
            backButton.Enabled = true;
        }

        private void DisableBackButton()
        {
            backButton.Enabled = false;
        }

        private void DisableCancelButton()
        {
            backButton.Enabled = false;
        }

        private void CreateDesktopShortcut()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string shortcutLocation = Path.Combine(path, Constants.SHORTCUT_NAME);
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = Constants.PERUN2;
            shortcut.TargetPath = Paths.GetInstance().GetGuiPath();
            shortcut.Save();
        }

        private void CreateMenuStartShortcut()
        {
            string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", Constants.PERUN2);

            if (!Directory.Exists(appStartMenuPath))
                Directory.CreateDirectory(appStartMenuPath);

            string shortcutLocation = Path.Combine(appStartMenuPath, Constants.SHORTCUT_NAME);
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = Constants.PERUN2;
            shortcut.TargetPath = Paths.GetInstance().GetGuiPath();
            shortcut.Save(); 
        }
    }
}
