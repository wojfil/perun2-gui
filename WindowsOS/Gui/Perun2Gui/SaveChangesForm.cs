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
    public partial class SaveChangesForm : Form
    {
        private MainForm _MainForm;
        private bool CancelledExit = false;

        public SaveChangesForm(MainForm mainForm)
        {
            _MainForm = mainForm;
            InitializeComponent();
            this.Icon = Resources.perun256;
            DarkMode();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            _MainForm.SaveToCurrentFile();
            this.Close();
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.CancelledExit = true;
            this.Close();
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);
            this.mainLabel.BackColor = Color.FromArgb(30, 30, 30);
        }

        public bool IsExitCancelled()
        {
            return CancelledExit;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.CancelledExit = true;
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
