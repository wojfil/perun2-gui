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

namespace Perun2Installer
{
    public partial class MainForm : Form
    {
        private static readonly string LOAD_PATH_TITLE = "Select a directory";


        private void SetDefaultLicense()
        {
            LicenseKey = String.Empty;
        }

        private void SetDefaultInstallationPath()
        {
            SetInstallationPath(ProgramFilesx86());
        }


        private string ProgramFilesx86()
        {
            /*if (8 == IntPtr.Size // check for 64-bit os
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }*/

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        private void LoadInstallationPath()
        {
            DirectorySelectDialog dialog;
            CheckInstallationPathExistence();

            if (Paths.GetInstance().HasPath())
            {
                dialog = new DirectorySelectDialog
                {
                    InitialDirectory = Paths.GetInstance().GetRootPath(),
                    Title = LOAD_PATH_TITLE
                };
            }
            else
            {
                dialog = new DirectorySelectDialog
                {
                    Title = LOAD_PATH_TITLE
                };
            }


            if (dialog.Show(Handle))
            {
                SetInstallationPath(dialog.FileName);
            }
        }

        private void CheckInstallationPathExistence()
        {
            if (!Paths.GetInstance().RootExists())
            {
                pathBox.Text = String.Empty;

                Paths.GetInstance().ErasePath();

                if (PageManager.GetCurrentPanel() == panelPath)
                {
                    nextButton.Enabled = false;
                }
            }
        }

        private void SetInstallationPath(string path)
        {
            bool exists = Directory.Exists(path);
            if (exists)
            {
                pathBox.Text = PathShortcut(path, Constants.PATH_SHORTCUT_LENGTH);
                Paths.GetInstance().SetPath(path);
            }
            else
            {
                Paths.GetInstance().ErasePath();
            }

            if (PageManager.GetCurrentPanel() == panelPath)
            {
                nextButton.Enabled = exists;
            }
        }

        private string PathShortcut(string path, int maxLength)
        {
            int length = path.Length;

            if (length <= maxLength)
                return path;

            string right = path.Substring(length - (maxLength - 6));
            int index = right.IndexOf('\\');

            return index == -1
                ? path.Substring(0, 3) + "..." + right
                : path.Substring(0, 3) + "..." + right.Substring(index);
        }
    }
}
