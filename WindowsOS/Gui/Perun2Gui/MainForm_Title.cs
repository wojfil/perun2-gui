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

namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        private void InitSavedProgress()
        {
            IsCodeSaved = true;
            saveToolStripMenuItem.Enabled = false;
        }

        private void RefreshSavedProgress()
        {
            IsCodeSaved = state.HasFile()
               ? PrevCode.Equals(codeBox.Text)
               : String.IsNullOrEmpty(codeBox.Text);

            saveToolStripMenuItem.Enabled = !IsCodeSaved;
        }

        private void RefreshFormTitle()
        {
            if (state.HasFile())
            {
                if (IsCodeSaved)
                {
                    saveToolStripMenuItem.Enabled = false;
                    Text = state.FileNameString + " - " + Constants.LANGUAGE_NAME;
                }
                else
                {
                    saveToolStripMenuItem.Enabled = true;
                    Text = "*" + state.FileNameString + " - " + Constants.LANGUAGE_NAME;
                }
            }
            else
            {
                if (IsCodeSaved)
                {
                    saveToolStripMenuItem.Enabled = false;
                    Text = "Untitled - " + Constants.LANGUAGE_NAME;
                }
                else
                {
                    saveToolStripMenuItem.Enabled = true;
                    Text = "*Untitled - " + Constants.LANGUAGE_NAME;
                }
            }
        }
    }
}
