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
using System.Drawing;
using System.Windows.Forms;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFullScreen();
        }

        private void SetFullScreen()
        {
            this.WindowState = FormWindowState.Maximized;
            TryToResizeLogBox();
        }

        private void dayModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGuiTheme(GuiTheme.Day);
        }

        private void nightModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGuiTheme(GuiTheme.Night);
        }

        private void SetGuiTheme(GuiTheme guiTheme)
        {
            GuiTheme current = SavedSettings.GetInstance().GetGuiTheme();
            if (guiTheme == current)
                return;

            SavedSettings.GetInstance().SetGuiTheme(guiTheme);
        }

        private void fitScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FitScreen();
        }

        private void smallScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SmallScreen();
        }
    }
}
