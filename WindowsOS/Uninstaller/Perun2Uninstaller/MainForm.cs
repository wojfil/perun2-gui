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
            SetGraphics();
        }

        private void SetGraphics()
        {
            topStripPanel1.BackColor = Color.FromArgb(150, 150, 150);
            topStripPanel2.BackColor = Color.FromArgb(200, 200, 200);
            topStripPanel3.BackColor = Color.FromArgb(230, 230, 230);
            this.Icon = Resources.perun256;
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
