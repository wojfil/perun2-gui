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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Perun2Manager
{
    public partial class ManagerRow : UserControl
    {
        public int Id;
        public string RowName;
        private MainForm MainForm;

        public ManagerRow(string name, int id, MainForm mainForm)
        {
            InitializeComponent();
            this.RowName = name;
            this.nameBox.Text = name + Constants.PERUN2_EXTENSION;
            this.Id = id;
            this.MainForm = mainForm;

        }

        public System.Windows.Forms.PictureBox GetFileImage()
        {
            return fileImageBox;
        }
        public System.Windows.Forms.TextBox GetNameBox()
        {
            return nameBox;
        }
        public System.Windows.Forms.Panel GetNamePanel()
        {
            return namePanel;
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                Filesystem.OpenScriptInGUI(RowName);
            }
            catch (Exception) {
                Popup.Error("Something wrong happened and this script file could not be opened.");
                return;
            }
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            var form = new RenameForm(MainForm, RowName);
            form.ShowDialog();
            if (form.DialogResult != DialogResult.Cancel)
            {
                MainForm.RefreshAll();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var path = Filesystem.GetPathToScript(RowName);

            if (!File.Exists(path))
            {
                Popup.Error("This script file no longer exists.");
                return;
            }

            try
            {
                File.Delete(path);
                MainForm.RefreshAll();
            }
            catch(Exception)
            {
                Popup.Error("Something wrong happened and the script file could not be deleted. It is probably opened in another program.");
            }
        }


    }
}
