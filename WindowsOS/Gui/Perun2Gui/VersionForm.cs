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
            RefreshGuiTheme();


            LoadData();

            oldBox.ContextMenu = new ContextMenu();
            newBox.ContextMenu = new ContextMenu();
        }

        private void RefreshGuiTheme()
        {
            this.BackColor = Constants.COLOR_FORMBACK_DAY;
            inputPanel.BackColor = Constants.COLOR_TEXTBACK_DAY;
            oldBox.BackColor = Constants.COLOR_TEXTBACK_DAY;
            outputPanel.BackColor = Constants.COLOR_TEXTBACK_DAY;
            newBox.BackColor = Constants.COLOR_TEXTBACK_DAY;
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
            string p1 = Paths.GetInstance().NEW_EXE_PATH;
            string p2 = Paths.GetInstance().NEW_GUI_PATH;
            string p3 = Paths.GetInstance().NEW_MANAGER_PATH;
            string p4 = Paths.GetInstance().NEW_UNINSTALL_PATH;

            try
            {
                if (File.Exists(p1)) { File.Delete(p1); }
                if (File.Exists(p2)) { File.Delete(p2); }
                if (File.Exists(p3)) { File.Delete(p3); }
                if (File.Exists(p4)) { File.Delete(p4); }
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
            // check 
            if (TooManyRunningProcesses() || !DeleteOldFiles())
            {
                return;
            }

            // download files from web
            try
            {
                DownloadFile(Constants.ACTUALIZATION_FILE_PERUN2,     Paths.GetInstance().NEW_EXE_PATH);
                DownloadFile(Constants.ACTUALIZATION_FILE_UNINSTALL,  Paths.GetInstance().NEW_UNINSTALL_PATH);
                DownloadFile(Constants.ACTUALIZATION_FILE_GUI,        Paths.GetInstance().NEW_GUI_PATH);
                DownloadFile(Constants.ACTUALIZATION_FILE_MANAGER,    Paths.GetInstance().NEW_MANAGER_PATH);
            }
            catch (Exception e)
            {
                Popup.Error("Actualization has failed (network connection). " + e.Message);
                return;
            }

            // close this program and run a batch script that finally renames downloaded exe files
            // why not do this here in C#?
            // a running program cannot rename itself
            RunActualizeBatch();
        }

        private void RunActualizeBatch()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe",
                    String.Format("/k {0} & {1} & {2}",
                        "timeout /T 2 /NOBREAK >NUL",
                        "\"" + Paths.GetInstance().ACTUALIZE_BATCH_PATH + "\"",
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
