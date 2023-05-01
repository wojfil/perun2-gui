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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.Icon = Resources.perun256;
            infoBox.ContextMenu = new ContextMenu();

            LoadInfoText();
            RefreshGuiTheme();
        }

        private void LoadInfoText()
        {
            string version;

            if (DataApi.GetCurrentVersion(out version))
            {
                version = "Version " + version;
            }
            else
            {
                version = "Unknown Version";
            }

            infoBox.Text = "Perun2" + Environment.NewLine
                + version + Environment.NewLine
                + "GNU General Public License v3.0";
        }

        private void RefreshGuiTheme()
        {
            this.BackColor = Constants.COLOR_FORMBACK_DAY;
            infoPanel.BackColor = Constants.COLOR_TEXTBACK_DAY;
            infoBox.BackColor = Constants.COLOR_TEXTBACK_DAY;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = logoBox;
        }
    }
}
