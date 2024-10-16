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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using Perun2Gui.Properties;


namespace Perun2Gui
{
    public partial class VersionForm : Form
    {
        private string CurrentVersion;
        private string LatestVersion;
        private bool LatestVersionLoaded;
        private bool Actualizable;

        public VersionForm()
        {
            InitializeComponent();
            this.Icon = Resources.perun256;

            LoadData();

            oldBox.ContextMenu = new ContextMenu();
            newBox.ContextMenu = new ContextMenu();
            DarkMode();
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);
            this.oldBox.BorderStyle = BorderStyle.None;
            this.oldPanel.BackColor = this.oldBox.BackColor;
            this.newBox.BorderStyle = BorderStyle.None;
            this.newPanel.BackColor = this.newBox.BackColor;

            this.label1.BackColor = Color.FromArgb(30, 30, 30);
            this.label2.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void LoadData()
        {
            // load data about current version
            bool exists = File.Exists(Paths.GetInstance().EXE_PATH);
            if (exists)
            {
                string curr;
                if (DataApi.GetCurrentVersion(out curr))
                {
                    CurrentVersion = curr;
                    oldBox.Text = curr;
                    this.Text = "Perun2, ver. " + curr;
                }
                else
                {
                    NoCurrentData();
                }
            }
            else
            {
                NoCurrentData();
            }

            // load data about the latest version
            string latest;
            if (DataApi.GetLatestVersion(out latest))
            {
                LatestVersion = latest;
                LatestVersionLoaded = true;
                newBox.Text = latest;
            }
            else
            {
                LatestVersion = "failed";
                LatestVersionLoaded = false;
                newBox.Text = LatestVersion;
            }

            Actualizable = LatestVersionLoaded && !CurrentVersion.Equals(LatestVersion);
            actualizeButton.Enabled = Actualizable;
        }

        private void NoCurrentData()
        {
            CurrentVersion = "failed";
            oldBox.Text = CurrentVersion;
            this.Text = "Perun2 with errors";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void actualizeButton_Click(object sender, EventArgs e)
        {
            if (Actualizable)
            {
                Actualize();
            }
        }

        private int CountProcesses(string FullPath)
        {
            int count = 0;
            string FilePath = Path.GetDirectoryName(FullPath);
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();

            Process[] pList = Process.GetProcessesByName(FileName);

            foreach (Process p in pList)
            {
                if (p.MainModule.FileName.StartsWith(FilePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    count++;
                }
            }

            return count;
        }

        private bool DeleteOldFiles()
        {
            string path = Paths.GetInstance().INSTALLATION_PATH;

            try
            {
                if (File.Exists(path)) { File.Delete(path); }
            }
            catch (Exception)
            {
                Popup.Error("Actualization has failed. Obsolete files could not be deleted.");
                return false;
            }

            return true;
        }

        private bool TooManyRunningProcesses()
        {
            int p1 = CountProcesses(Paths.GetInstance().EXE_PATH);
            int p2 = CountProcesses(Paths.GetInstance().GUI_PATH);
            int p3 = CountProcesses(Paths.GetInstance().MANAGER_PATH);
            int p4 = CountProcesses(Paths.GetInstance().UNINSTALL_PATH);

            if (p1 != 0)
            {
                Popup.Error("Currently an instance of 'perun2.exe' is running in the background, so Perun2 cannot be actualized.");
                return true;
            }

            if (p2 != 1)
            {
                Popup.Error("Currently there are at least two Gui applications opened, so Perun2 cannot be actualized. You should close the others first.");
                return true;
            }

            if (p3 != 0)
            {
                Popup.Error("Perun2 Manager is currently running in the background, so it cannot be actualized. You should close it first.");
                return true;
            }

            if (p4 != 0)
            {
                Popup.Error("Uninstaller is currently running in the background, so it cannot be actualized. You should close it first.");
                return true;
            }

            return false;
        }

        private void DownloadFile(string url, string destination)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(url, destination);
            }
        }
        
        private void Actualize()
        {
            if (TooManyRunningProcesses() || !DeleteOldFiles())
            {
                return;
            }

            try
            {
                DownloadFile(Constants.INSTALLATION_FILE_PERUN2, Paths.GetInstance().INSTALLATION_PATH);
            }
            catch (Exception e)
            {
                Popup.Error("Actualization has failed (network connection). " + e.Message);
                return;
            }

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe",
                    String.Format("/k {0} & {1} & {2}",
                        "timeout /T 2 /NOBREAK >NUL",
                        "\"" + Paths.GetInstance().INSTALLATION_PATH + "\"",
                        "exit"
                    )
                );
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                Process.Start(psi);
            }
            catch (Exception)
            {
                Popup.Error("Something went wrong.");
                return;
            }

            Application.Exit();
        }
    }
}
