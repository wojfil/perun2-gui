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
using Perun2Gui.Properties;

namespace Perun2Gui
{
    public partial class TermsForm : Form
    {
        public TermsForm()
        {
            InitializeComponent();
            LoadLicenseText();
            DarkMode();
            this.Icon = Resources.perun256;
            licenseBox.ContextMenu = new ContextMenu();
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);
            this.licenseBox.BorderStyle = BorderStyle.None;
            this.licensePanel.BackColor = this.licenseBox.BackColor;
        }

        private void LoadLicenseText()
        {
            licenseBox.Text = Resources.license.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TermsForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = licensePanel;
        }


    }
}
