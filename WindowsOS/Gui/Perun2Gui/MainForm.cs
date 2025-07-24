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
using FastColoredTextBoxNS;
using Perun2Gui.Properties;
using Perun2Gui.Hint;
using static System.Windows.Forms.AxHost;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        private const string LINE = "──────────────────────────────────";
        private string NEW_LINE = Environment.NewLine;

        private bool IsCodeSaved;
        private string PrevCode;
        private State state;


        // open application from Menu Start or directly
        public MainForm()
        {
            state = new State();

            PreInit();
            InitializeComponent();

            PrevCode = null;
            SaveAsEnabled = false;

            Init();
        }

        // open application inside a directory
        public MainForm(string location)
        {
            state = new State();

            PreInit();
            InitializeComponent();

            PrevCode = null;
            SaveAsEnabled = false;

            Init();
            SetLocation(location);
            enterLocationToolStripMenuItem.Enabled = true;
        }

        // open a source file
        public MainForm(string file, string code)
        {
            state = new State();

            PreInit();
            InitializeComponent();

            PrevCode = null;
            SaveAsEnabled = !String.IsNullOrEmpty(code);

            Init();
            SetSourceFile(file, code, false);
            try
            {
                SavedSettings.GetInstance().AddRecentFile(file);
            }
            catch (Exception) { }
        }

        private void PreInit()
        {
            InitAho();
            LockedTextChange = true;
        }

        private void Init()
        {
            this.Icon = Resources.perun256;
            InitSavedProgress();
            PrevRecentFiles = new List<string>();
            ResizeEnabled = true;
            LogsRecentlyCleaned = false;
            PrevWindowState = this.WindowState;
            WholeScreen = Screen.FromControl(this).WorkingArea;

            codeBox.Select();
            InitHistory();
            InitMenu();
            InitRun();
            InitLogs();
            RefreshNoOmit();
            RefreshLineCol();
            RefreshFlagsLabel();
            omitPeruFilesToolStripMenuItem.Checked = SavedSettings.GetInstance().GetNoOmit();
            flagSToolStripMenuItem.Checked = SavedSettings.GetInstance().GetSilent();

            logBackgroundWorker.DoWork += (sender, e) => WorkerStart();
            logBackgroundWorker.RunWorkerCompleted += (sender, e) => WorkerFinish();

            DarkMode();
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);
            this.logBox.BorderStyle = BorderStyle.None;
            this.locationBox.BorderStyle = BorderStyle.None;
            this.fileBox.BorderStyle = BorderStyle.None;
            this.locationPanel.BackColor = this.locationBox.BackColor;
            this.filePanel.BackColor = this.fileBox.BackColor;
            this.logPanel.BackColor = this.logBox.BackColor;
            this.locPictureBox.BackColor = Color.Transparent;
            this.filePictureBox.BackColor = Color.Transparent;
            this.codeBox.CaretColor = Color.White;
            this.codeBox.CaretBlinking = false;
        }

        private void InitLogs()
        {
            string logs = GetInitLogs();
            logBox.Text = logs;
        }

        private string GetInitLogs()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Welcome to " + Constants.LANGUAGE_NAME);
            sb.Append(NowToString());
            sb.Append(NEW_LINE);

            if (Constants.ACTUALIZATIONS_ENABLED)
            {
                AppendActualizationInfo(sb);
            }

            sb.Append(LINE);
            return sb.ToString();
        }

        private void AppendActualizationInfo(StringBuilder sb)
        {
            string currrentVersion;
            if (!DataApi.GetCurrentVersion(out currrentVersion))
            {
                return;
            }

            string latestVersion;
            if (!DataApi.GetLatestVersion(out latestVersion))
            {
                return;
            }

            if (currrentVersion.Equals(latestVersion))
            {
                return;
            }

            sb.AppendLine("Possible actualization");
            sb.AppendLine("(" + currrentVersion + " -> " + latestVersion + ")");
        }

        private string NowToString()
        {
            StringBuilder sb = new StringBuilder();

            DateTime now = DateTime.Now;
            sb.Append(now.Day);
            sb.Append(' ');
            sb.Append(Constants.MONTH_NAMES[now.Month - 1]);
            sb.Append(' ');
            sb.Append(now.Year);
            sb.Append(", ");
            sb.Append(FilledClockUnit(now.Hour));
            sb.Append(':');
            sb.Append(FilledClockUnit(now.Minute));
            sb.Append(':');
            sb.Append(FilledClockUnit(now.Second));

            return sb.ToString();
        }

        private string NowToNarrowString()
        {
            StringBuilder sb = new StringBuilder();

            DateTime now = DateTime.Now;
            sb.Append(now.Year);
            sb.Append(FilledClockUnit(now.Month));
            sb.Append(FilledClockUnit(now.Day));
            sb.Append(FilledClockUnit(now.Hour));
            sb.Append(FilledClockUnit(now.Minute));
            sb.Append(FilledClockUnit(now.Second));

            return sb.ToString();
        }

        public static string FilledClockUnit(int value)
        {
            return value <= 9
                ? "0" + value.ToString()
                : value.ToString();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Running)
            {
                RunStop();
            }

            if (IsProgressNotSaved())
            {
                AskForSaveChanges(e);
            }
        }

        private void AskForSaveChanges(FormClosingEventArgs e)
        {
            var form = new SaveChangesForm(this);
            form.ShowDialog();
            if (e != null && form.IsExitCancelled())
            {
                e.Cancel = true;
            }
        }

       private void topMenuStrip_MenuActivate(object sender, EventArgs e)
       {
           SetNullHint();
       }
    }
}
