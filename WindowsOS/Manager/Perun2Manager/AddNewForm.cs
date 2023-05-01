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
    public partial class AddNewForm : Form
    {
        private MainForm MainForm;

        public AddNewForm(MainForm mainForm)
        {
            MainForm = mainForm;
            InitializeComponent();
            InitStyle();
        }

        private void InitStyle()
        {
            BackColor = Constants.COLOR_FORMBACK_DAY;
            this.namePanel.BackColor = Color.White;
            this.nameBox.BackColor = Color.White;
            this.Icon = Resources.perun256;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string name = nameBox.Text.Trim();

            if (!name.IsValidPerun2ScriptName())
            {
                return;
            }

            string path = Filesystem.GetPathToScript(name);

            try
            {
                File.Create(path).Dispose();
                Filesystem.OpenScriptInGUI(name);
            }
            catch (Exception)
            {
                Popup.Error("Something wrong happened and a new script file could not be created.");
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
