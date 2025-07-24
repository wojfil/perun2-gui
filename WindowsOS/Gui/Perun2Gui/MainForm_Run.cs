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
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;



namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        private bool Running;
        private bool Stopped;

        private delegate void RunButtonDelegate();
        private delegate void CodeBoxDelegate();
        private delegate void TopMenuDelegate();
        private delegate void MainFormDelegate();
        private delegate void LogBoxDelegate();
        private delegate void WaitingDelegate();

        private object SyncGate = new object();
        private Process Process;

        private static readonly string CHECK_FLAG = "-m";
        private static readonly string NOOMIT_FLAG = "-gn";
        private static readonly string SILENT_FLAG = "-gs";
        private static readonly string NOOMIT_SILENT_FLAG = "-gns";
        private static readonly string NO_FLAG = "-g";

        internal const int CTRL_C_EVENT = 0;
        [DllImport("kernel32.dll")]
        internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool FreeConsole();
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        delegate Boolean ConsoleCtrlDelegate(uint CtrlType);

        private const int LOG_SLEEP = 25;
        private StringBuilder WaitingLogs = new StringBuilder();
        private bool AnyWaitingLogs = false;
        object LogGate = new object();

        
        private void InitRun()
        {
            Running = false;
            Stopped = false;
            RefreshRunButton();
        }

        private void RenameRunButton()
        {
            if (Stopped)
                runButton.Text = "...";
            else if (Running)
                runButton.Text = "Stop";
            else
                runButton.Text = "Run";
        }

        void RefreshRunButton()
        {
            if (runButton.InvokeRequired)
            {
                var d = new RunButtonDelegate(RenameRunButton);
                runButton.Invoke(d);
            }
            else
            {
                RenameRunButton();
            }
        }

        private void RunStart(ExecutionMode mode)
        {
            if (Running || Stopped)
            {
                return;
            }

            if (mode == ExecutionMode.Run)
            {
                if (! state.HasLocation() && ! LoadLocation())
                {
                    return;
                }
            }

            string code = codeBox.Text;
            if (code.Length == 0) {
                return;
            }

            if (! state.HasFile())
            {
                if (!code.Equals(PrevCode))
                {
                    PrevCode = code;
                    SetBackup(code);
                }
            }
            else
            {
                if (state.HasBackup())
                {
                    if (!code.Equals(PrevCode))
                    {
                        PrevCode = code;
                        SetBackup(code);
                    }
                }
                else
                {
                    PrevCode = code;
                    SetBackup(code);
                }
            }

            Run(mode);
        }

        private void SetBackup(string code)
        {
            string backups = Paths.GetInstance().GetBackupsPath();
            Directory.CreateDirectory(backups);
            state.BackupPathString = Path.Combine(backups, GetNewBackupName() + Constants.PERUN2_EXTENSION);
            File.Create(state.BackupPathString).Dispose();
            File.WriteAllText(state.BackupPathString, code);
        }

        private string GetNewBackupName()
        {
            string now = NowToString().Replace(':', '-');
            string rawName = now;

            if (!File.Exists(rawName))
                return rawName;

            rawName += '_';
            int index = 2;
            string name = rawName + index;

            while (File.Exists(name))
            {
                index++;
                name = rawName + index;
            }

            return name;
        }

        private bool IsCurrentFileGlobalScript()
        {
            if (! state.HasFile())
            {
                return false;
            }

            string s1 = Paths.GetInstance().GetRootPath();
            string s2 = Path.GetDirectoryName(state.LocationPathString);
            return String.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        private string GetRunnerArgs(ExecutionMode mode)
        {
            StringBuilder sb = new StringBuilder();

            bool noomit = SavedSettings.GetInstance().GetNoOmit();
            bool silent = SavedSettings.GetInstance().GetSilent();

            if (mode == ExecutionMode.CheckCorrectness)
            {
                sb.Append(CHECK_FLAG);
                sb.Append(' ');
            }

            if (noomit && silent)
            {
                sb.Append(NOOMIT_SILENT_FLAG);
                sb.Append(' ');
            }
            else if (noomit)
            {
                sb.Append(NOOMIT_FLAG);
                sb.Append(' ');
            }
            else if (silent)
            {
                sb.Append(SILENT_FLAG);
                sb.Append(' ');
            }
            else
            {
                sb.Append(NO_FLAG);
                sb.Append(' ');
            }

            sb.Append('"');
            sb.Append(state.HasBackup() ? state.BackupPathString : state.FilePathString);
            sb.Append('"');

            if (mode == ExecutionMode.Run)
            {
                sb.Append(" -d ");
                sb.Append('"');
                sb.Append(state.LocationPathString.EndsWith("\\") ? (state.LocationPathString + "\\") : state.LocationPathString);
                sb.Append('"');
            }

            return sb.ToString();
        }

        private void EnableSaveLogs()
        {
            saveLogsToolStripMenuItem.Enabled = true;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunStart(ExecutionMode.Run);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunStop();
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunStart(ExecutionMode.CheckCorrectness);
        }

        private void RefreshNoOmit()
        {
            omitPeruFilesToolStripMenuItem.Checked = SavedSettings.GetInstance().GetNoOmit();
        }

        private void RefreshSilent()
        {
            flagSToolStripMenuItem.Checked = SavedSettings.GetInstance().GetSilent();
        }

        private void flagSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavedSettings.GetInstance().ReverseSilent();
            RefreshSilent();
            RefreshFlagsLabel();
        }

        private void omitPeruFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavedSettings.GetInstance().ReverseNoOmit();
            RefreshNoOmit(); 
            RefreshFlagsLabel();
        }

        private void RefreshFlagsLabel()
        {
            if (SavedSettings.GetInstance().GetNoOmit())
            {
                flagsLabel.Text = SavedSettings.GetInstance().GetSilent()
                    ? "-ns"
                    : "-n";
            }
            else
            {
                flagsLabel.Text = SavedSettings.GetInstance().GetSilent()
                    ? "-s"
                    : "";
            }
        }

        private void RunStartRefreshForm()
        {
            codeBox.Enabled = false;

            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
            cutToolStripMenuItem.Enabled = false;
            pasteToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
            clearLogsToolStripMenuItem.Enabled = false;
            clearCodeToolStripMenuItem.Enabled = false;
            replaceToolStripMenuItem.Enabled = false;
            selectAllToolStripMenuItem.Enabled = false;

            smallScreenToolStripMenuItem.Enabled = false;
            fitScreenToolStripMenuItem.Enabled = false;
            fullScreenToolStripMenuItem.Enabled = false;
            this.MaximizeBox = false;
            DisableResizeByUser();

            runToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
            checkToolStripMenuItem.Enabled = false;

            newToolStripMenuItem.Enabled = false;
            relocateToolStripMenuItem.Enabled = false;
            openToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            saveLogsToolStripMenuItem.Enabled = false;
            recentFilesToolStripMenuItem.Enabled = false;
            exitToolStripMenuItem.Enabled = false;
            omitPeruFilesToolStripMenuItem.Enabled = false;
            flagSToolStripMenuItem.Enabled = false;

            licenseToolStripMenuItem.Enabled = false;
            versionToolStripMenuItem.Enabled = false;
            aboutToolStripMenuItem.Enabled = false;

            changePathToolStripMenuItem.Enabled = false;
        }

        private void RunStopRefreshForm()
        {
            if (codeBox.InvokeRequired)
            {
                var d = new CodeBoxDelegate(EnableCodeBox);
                codeBox.Invoke(d);
            }
            else
            {
                EnableCodeBox();
            }

            if (topMenuStrip.InvokeRequired)
            {
                var d = new TopMenuDelegate(RefreshTopMenuStripAfterRun);
                topMenuStrip.Invoke(d);
            }
            else
            {
                RefreshTopMenuStripAfterRun();
            }
        }

        private void RefreshTopMenuStripAfterRun()
        {
            runToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
            checkToolStripMenuItem.Enabled = true;

            newToolStripMenuItem.Enabled = true;
            relocateToolStripMenuItem.Enabled = true;
            openToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = !String.IsNullOrEmpty(codeBox.Text);
            saveLogsToolStripMenuItem.Enabled = true;
            recentFilesToolStripMenuItem.Enabled = true;
            exitToolStripMenuItem.Enabled = true;
            omitPeruFilesToolStripMenuItem.Enabled = true;
            flagSToolStripMenuItem.Enabled = true;

            smallScreenToolStripMenuItem.Enabled = true;
            fitScreenToolStripMenuItem.Enabled = true;
            fullScreenToolStripMenuItem.Enabled = true;

            licenseToolStripMenuItem.Enabled = true;
            versionToolStripMenuItem.Enabled = true;
            aboutToolStripMenuItem.Enabled = true;
            this.MaximizeBox = true;
            EnableResizeByUser();

            EnableClearLogs();
            RefreshPasteEnableness();
            RefreshHistoryMenuItems();
            RefreshSelectionMenuItems();
            RefreshCodeAlterEnableness();
            RefreshSelectAllEnableness();

            changePathToolStripMenuItem.Enabled = true;
        }

        private void EnableCodeBox()
        {
            codeBox.Enabled = true;
        }

        private void omitInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new OptionsInfoForm();
            form.ShowDialog();
        }
    }
}
