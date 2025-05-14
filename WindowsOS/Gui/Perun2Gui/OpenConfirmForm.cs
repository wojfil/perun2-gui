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
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Perun2Gui.Properties;

namespace Perun2Gui
{
    public partial class OpenConfirmForm : Form
    {

        public OpenConfirmForm(int amount)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.Icon = Resources.perun256;
            DarkMode();

            string text = "Are you sure to open " + amount + " elements? That's a lot.";
            this.mainLabel.Text = text;
            this.mainLabel.Left = (this.ClientSize.Width - this.mainLabel.Width) / 2;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);
            this.mainLabel.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
