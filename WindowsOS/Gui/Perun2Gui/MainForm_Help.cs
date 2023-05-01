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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;


namespace Perun2Gui
{
    public partial class MainForm : Form
    {

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Website.Show(Constants.DOCS_ADDRESS, "Failed to enter the documentation website.");
        }

        private void backupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Paths.GetInstance().GetBackupsPath();
            if (Directory.Exists(path))
            {
                try
                {
                    EnterDirectory(path);
                }
                catch(Exception)
                {
                    Popup.Error("Failed to enter the backup directory.");
                }
            }
            else
            {
                Popup.Error("The backup directory does not exist.");
            }
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TermsForm();
            form.ShowDialog();
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new VersionForm();
            form.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

    }
}
