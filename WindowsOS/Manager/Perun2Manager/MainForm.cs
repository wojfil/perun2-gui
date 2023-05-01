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
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Perun2Manager.Properties;

namespace Perun2Manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitStyle();
            pathBox.Text = StringUtil.PathAbbreviation(Paths.GetInstance().GetScriptsPath(), Constants.PATH_ABBREVIATION_LENGTH);
            UnselectAll();
            RefreshAll();
        }

        public void UnselectAll()
        {
            label1.Select();
        }

        private void InitStyle()
        {
            this.BackColor = Constants.COLOR_FORMBACK_DAY;
            this.outerPanel.BackColor = Constants.COLOR_FORMBACK_DAY;
            this.mainPanel.BackColor = Constants.COLOR_FORMBACK_DAY;
            this.pathPanel.BackColor = Constants.COLOR_TEXTBACK_DAY;
            this.pathBox.BackColor = Constants.COLOR_TEXTBACK_DAY;
            this.Icon = Resources.perun256;
        }

        void AddRow(string name)
        {
            int id = tablePanel.Controls.Count;
            ManagerRow mr = new ManagerRow(name, id, this);
            tablePanel.Controls.Add(mr);
            tablePanel.SetRow(mr, id);
        }

        void ClearAndSetNewRows(List<string> names)
        {
            tablePanel.Controls.Clear();

            foreach (var n in names)
            {
                AddRow(n);
            }
        }

        public void RefreshAll()
        {
            EnsureScriptsDirectoryExists();

            var keys = RegistryAction.GetRegistryKeys();
            var keyNames = keys.TrimPrefix(Constants.KEYNAME_TOTAL_PREFIX_LENGTH);
            var files = Filesystem.GetGlobalScriptFiles();
            ClearAndSetNewRows(files);

            if (keyNames.EqualsToList(files))
            {
                return;
                // if registry keys are the same as script files
                // there is nothing to do
                // just escape from here
            }

            RegistryAction.ClearAndSetNewKeys(keys, files);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void enterPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnterDirectory(Paths.GetInstance().GetScriptsPath());
        }

        private void EnterDirectory(string path)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = path,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception)
            {
                Popup.Error("Failed to open directory '" + path + "'");
            }
        }

        private void addNewButton_Click(object sender, EventArgs e)
        {
            var files = Filesystem.GetGlobalScriptFiles();
            if (files.Count >= Constants.CONTEXT_MENU_LIMIT)
            {
                Popup.Error("There is a limit of " + Constants.CONTEXT_MENU_LIMIT + " elements in the context menu. You cannot add more items right now. "
                    + "This is fault of the operating system and cannot be bypassed. Blame Gates.");

                return;
            }

            var form = new AddNewForm(this);
            form.ShowDialog();
            if (form.DialogResult != DialogResult.Cancel) 
            {
                RefreshAll();
            }
        }

        private void EnsureScriptsDirectoryExists()
        {
            var path = Paths.GetInstance().GetScriptsPath();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void seeAllButton_Click(object sender, EventArgs e)
        {
            EnsureScriptsDirectoryExists();
            
            foreach (var file in Filesystem.GetGlobalScriptFiles())
            {
                Filesystem.OpenScriptInGUI(file);
            }
        }
    }
}
