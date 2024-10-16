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
using Perun2Manager.Properties;

namespace Perun2Manager
{
    public partial class RenameForm : Form
    {
        private MainForm MainForm;
        private string OldName;


        public RenameForm(MainForm mainForm, string oldName)
        {
            MainForm = mainForm;
            OldName = oldName;
            InitializeComponent();
            this.Icon = Resources.perun256;
            DarkMode();
            this.nameBox.Text = OldName;
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);

            this.nameBox.BorderStyle = BorderStyle.None;
            this.namePanel.BackColor = this.nameBox.BackColor;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string newName = nameBox.Text.Trim();
            string oldPath = Filesystem.GetPathToScript(OldName);
            string newPath = Filesystem.GetPathToScript(newName);

            if (!newName.IsValidPerun2ScriptName())
            {
                return;
            }
            
            if (!File.Exists(oldPath))
            {
                Popup.Error("This script file no longer exists.");
                return;
            }
            
            try
            {
                File.Move(oldPath, newPath);
            }
            catch (Exception)
            {
                Popup.Error("Something wrong happened and this script file could not be renamed.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}
