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
using Perun2Uninstaller.Properties;
using System.IO;

namespace Perun2Uninstaller
{
    public partial class MainForm : Form
    {
        private Actions.ActionChain Actions = new Actions.ActionChain();

        public MainForm()
        {
            InitializeComponent();
            this.Icon = Resources.perun256;
            DarkMode();
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);
            this.logoBox.BackColor = Color.Transparent;
            this.bottomPanel.BackColor = Color.FromArgb(30, 30, 30);
            this.bottomPanel.BorderStyle = BorderStyle.FixedSingle;

            this.label1.BackColor = Color.FromArgb(30, 30, 30);
            this.label2.BackColor = Color.FromArgb(30, 30, 30);
            this.label3.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            Uninstall();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
