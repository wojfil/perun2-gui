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
    public partial class OptionsInfoForm : Form
    {
        public OptionsInfoForm()
        {
            InitializeComponent();
            this.Icon = Resources.perun256;
            RefreshGuiTheme();

            omitBox.Text =
              "-n   Iterate over all filesystem elements, no exceptions." + Environment.NewLine
            + "-s   No command log messages." + Environment.NewLine + Environment.NewLine
            + "You can turn on these two command-line options in order to change interpreter's behavior. " + Environment.NewLine
            + "Filesystem exceptional elements are: Perun2 own scripts files (*.peru) and version control directories (name .git or .svn). " + Environment.NewLine
            + "If you want to know more about the interface of Perun2, run 'perun2 --help' in the command-line.";
        }

        private void RefreshGuiTheme()
        {
            this.BackColor = Constants.COLOR_FORMBACK_DAY;
            omitBox.BackColor = Constants.COLOR_TEXTBACK_DAY;
            omitPanel.BackColor = Constants.COLOR_TEXTBACK_DAY;
        }

        private void OmitInfoForm_Load(object sender, EventArgs e)
        {
           this.ActiveControl = omitPanel;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
