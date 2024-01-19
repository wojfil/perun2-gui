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
using System.Diagnostics;
using System.Windows.Forms;


namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        private static readonly string SCRIPT_FILTHER = "Perun2|*.peru";
        private static readonly string LOG_FILTHER = "Text|*.txt";
        private static readonly string LOAD_LOCATION_TITLE = "Select a directory";
        private static readonly string OPEN_FILE_TITLE = "Open a Perun2 file";
        private static readonly string SAVE_AS_TITLE = "Save as";
        private static readonly string NEW_TITLE = "New file";

        private const int LOCATION_SHOW_MAX_LENGTH = 30;
        private const int FILE_SHOW_MAX_LENGTH = 30;


        private List<string> PrevRecentFiles;
        private bool SaveAsEnabled;


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsCodeSaved && HasFile)
            {
                AskForSaveChanges(null);
            }

            var dialog = new SaveFileDialog();
            dialog.Filter = SCRIPT_FILTHER;
            dialog.Title = NEW_TITLE;

            if (HasFile)
            {
                dialog.FileName = FileNameString;
            }

            if (HasLocation && Directory.Exists(LocationString))
            {
                dialog.InitialDirectory = LocationString;
                dialog.RestoreDirectory = true;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                if (!path.Equals(""))
                {
                    try
                    {
                        File.WriteAllText(path, String.Empty);
                        SetSourceFile(path, String.Empty, true);
                        SavedSettings.GetInstance().AddRecentFile(path);
                    }
                    catch (Exception)
                    {
                        if (File.Exists(path)) 
                        {
                            string name = Path.GetFileName(path);
                            Popup.Error("Something went wrong while overriding script file '" + name + "'.");
                        }
                        else
                        {
                            Popup.Error("Something went wrong while creating a new script file.");
                        }
                    }
                }
            }
        }

        private void relocateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLocation();
            IsCodeSaved = false;
            RefreshFormTitle();
        }

        private bool LoadLocation()
        {
            DirectorySelectDialog dialog;

            if (HasLocation)
            {
                dialog = new DirectorySelectDialog
                {
                    InitialDirectory = LocationString,
                    Title = LOAD_LOCATION_TITLE
                };
            }
            else
            {
                dialog = new DirectorySelectDialog
                {
                    Title = LOAD_LOCATION_TITLE
                };
            }


            if (dialog.Show(Handle))
            {
                enterLocationToolStripMenuItem.Enabled = true;

                SetLocation(dialog.FileName);
                SetFile(null);
                InitSavedProgress();
                RefreshFormTitle();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetFile(string file)
        {
            HasFile = file != null;
            FileString = file;
            FileNameString = Path.GetFileName(file);
            fileBox.Text = file == null ? String.Empty : StringUtil.NameShortcut(FileNameString, FILE_SHOW_MAX_LENGTH);
        }

        private void SetLocation(string location)
        {
            HasLocation = location != null;
            LocationString = location;
            locationBox.Text = location == null ? String.Empty : StringUtil.PathShortcut(location, LOCATION_SHOW_MAX_LENGTH);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSourceFile();
        }

        private void OpenSourceFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = SCRIPT_FILTHER;
            dialog.RestoreDirectory = true;
            dialog.Title = OPEN_FILE_TITLE;

            if (HasLocation && Directory.Exists(LocationString))
            {
                dialog.InitialDirectory = LocationString;
                dialog.RestoreDirectory = true;
            }

            if (dialog.ShowDialog() == DialogResult.OK && !dialog.FileName.Equals(""))
            {
                string path = dialog.FileName;
                string name = Path.GetFileName(path);

                try
                {
                    string code = File.ReadAllText(path);
                    IsCodeSaved = true;
                    SetSourceFile(path, code, true);
                    SavedSettings.GetInstance().AddRecentFile(path);
                }
                catch (Exception)
                {
                    Popup.Error("Something went wrong during opening file '" + name + "'.");
                }
            }
        }

        private void SetSourceFile(string path, string code, bool saveProgress)
        {
            if (HasFile && path.Equals(FileString) && code.Equals(codeBox.Text))
            {
                return;
            }

            if (saveProgress && IsProgressNotSaved())
            {
                AskForSaveChanges(null);
            }

            PrevCode = code;
            codeBox.Text = code;
            History.StartWithCode(code);
            RefreshHistoryMenuItems();

            SetFile(path);
            SetLocation(Path.GetDirectoryName(path));
            HasBackup = false;

            enterLocationToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            InitSavedProgress();
            RefreshFormTitle();
            FitScreen();
            MousePressed = false;
        }

        private bool IsProgressNotSaved()
        {
            return !IsCodeSaved && HasFile;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HasFile)
            {
                SaveToCurrentFile();
            }
            else
            {
                SaveAs();
            }
        }

        public void SaveToCurrentFile()
        {
            try
            {
                string code = codeBox.Text;

                if (!File.Exists(FileString))
                {
                    if (!Directory.Exists(LocationString))
                    {
                        Directory.CreateDirectory(LocationString);
                    }
                }

                File.WriteAllText(FileString, code);
                IsCodeSaved = false;
                HasBackup = false;
                PrevCode = code;
                InitSavedProgress();
                RefreshFormTitle();
            }
            catch (Exception)
            {
                Popup.Error("Something went wrong during saving code to '" + FileNameString + "'.");
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = SCRIPT_FILTHER;
            dialog.Title = SAVE_AS_TITLE;

            if (HasLocation && Directory.Exists(LocationString))
            {
                dialog.InitialDirectory = LocationString;
                dialog.RestoreDirectory = true;
            }

            try
            {
                string code = codeBox.Text;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.FileName;
                    if (!path.Equals(String.Empty))
                    {
                        if (!File.Exists(path))
                        {
                            string directory = Path.GetDirectoryName(path);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
                        }

                        File.WriteAllText(path, code);
                        SetSourceFile(path, code, false);
                        SavedSettings.GetInstance().AddRecentFile(path);
                    }
                }
            }
            catch (Exception)
            {
                Popup.Error("Something went wrong during saving script.");
            }
        }

        private void RefreshSaveAsEnableness()
        {
            if (SaveAsEnabled)
            {
                if (String.IsNullOrEmpty(codeBox.Text))
                {
                    SaveAsEnabled = false;
                    saveAsToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                if (!Running && !String.IsNullOrEmpty(codeBox.Text))
                {
                    SaveAsEnabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void saveLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = LOG_FILTHER;
            dialog.Title = "Save Logs";
            dialog.FileName = GetLogsFileName();

            if (HasLocation && Directory.Exists(LocationString))
            {
                dialog.InitialDirectory = LocationString;
                dialog.RestoreDirectory = true;
            }

            try
            {
                string logs = GetLogsToSave();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string name = dialog.FileName;
                    if (!name.Equals(""))
                    {
                        File.WriteAllText(name, logs);
                        Popup.Ok("Logs saved successfully.");
                    }
                }
            }
            catch (Exception)
            {
                Popup.Error("Something wrong happened during saving logs.");
            }
        }

        private string GetLogsFileName()
        {
            return NowToString().Replace(':', '-') + " Perun2 logs";
        }

        private string GetLogsToSave()
        {
            return logBox.Text + NEW_LINE + NowToString();
        }

        private void enterLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnterDirectory(LocationString);
        }

        private void turnIntoGlobalScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var files = Filesystem.GetGlobalScriptFiles();
            if (files.Count >= Constants.GLOBAL_SCRIPT_ITEMS_LIMIT)
            {
                Popup.Error("There is a limit of " + Constants.GLOBAL_SCRIPT_ITEMS_LIMIT + " elements in the context menu. You cannot add more items right now. "
                    + "This is fault of the operating system and cannot be bypassed. Blame Gates.");

                return;
            }

            var form = new GlobalScriptsForm(this);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                SetSourceFile(form.ScriptPath, codeBox.Text, false);
                SaveToCurrentFile();
            }
        }

        private void globalScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Paths.GetInstance().GetManagerPath();
            Process.Start(startInfo);
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            List<string> recentFiles = SavedSettings.GetInstance().GetRecentFiles();
            if (!PrevRecentFiles.SequenceEqual(recentFiles))
            {
                recentFilesToolStripMenuItem.DropDownItems.Clear();
                int length = recentFiles.Count();

                for (int i = 0; i < length; i++)
                {
                    string rf = recentFiles[i];
                    RecentFileMenuItem item = new RecentFileMenuItem(rf, i);
                    item.Click += new System.EventHandler(this.RecentFileClick);
                    recentFilesToolStripMenuItem.DropDownItems.Add(item);
                }

                PrevRecentFiles.Clear();
                PrevRecentFiles.AddRange(recentFiles);
            }
        }

        private void RecentFileClick(object sender, EventArgs e)
        {
            RecentFileMenuItem item = (sender as RecentFileMenuItem);
            string path = item.GetPath();
            int index = item.GetIndex();

            if (!File.Exists(path))
            {
                SavedSettings.GetInstance().DeleteRecentFile(index);
                string name = Path.GetFileName(path);
                Popup.Error("File '" + name + "' does not exist anymore.");
                return;
            }

            try
            {
                string code = File.ReadAllText(path);
                SavedSettings.GetInstance().RecentFileMoveFront(index);
                SetSourceFile(path, code, true);
            }
            catch (Exception)
            {
                string name = Path.GetFileName(path);
                Popup.Error("Something went wrong during opening file '" + name + "'");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EnterDirectory(string path)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
