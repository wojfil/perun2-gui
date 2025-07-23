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
using System.IO;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, 
            IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private int MousePressCaret = 0;
        private bool MousePressed = false;
        private string CodeBeforeChange;


        private void codeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextOverride)
            {
                TextOverride = false;
            }
            else if (!LockedTextChange)
            {
                if (!CodeBeforeChange.Equals(codeBox.Text))
                {
                    History.AddUnit();
                    RefreshHistoryMenuItems();
                }
            }

            RefreshSelectAllEnableness();
            RefreshCodeAlterEnableness();

            RefreshSavedProgress();
            RefreshFormTitle();
            RefreshSaveAsEnableness();
            RefreshBoxGraphics(sender as FastColoredTextBox);

            MousePressed = false;
            TryWindowWidening();
        }

        private void codeBox_TextChanging(object sender, TextChangingEventArgs e)
        {
            CodeBeforeChange = codeBox.Text;
        }

        private void codeBox_SelectionChanged(object sender, EventArgs e)
        {
            RefreshSelectionMenuItems();
            RefreshLineCol();
            RefreshHint();
        }

        private void codeBox_KeyPressing(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && codeBox.SelectionLength != 0)
            {
                TextOverride = true;
                History.SaveCurrentSelection();
            }
        }

        private void codeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (HasHint && e.KeyCode == Keys.Tab)
            {
                InsertWordFromHint();
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            SetNullHint();

            if (!Stopped)
            {
                if (Running)
                {
                    RunStop();
                }
                else if (codeBox.Text.Length != 0)
                {
                    RunStart(ExecutionMode.Run);
                }
            }
        }

        private void locationButton_Click(object sender, EventArgs e)
        {
            relocateToolStripMenuItem_Click(sender, e);
        }

        private void fileButton_Click(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_code_Click(object sender, EventArgs e)
        {
            if (undoToolStripMenuItem.Enabled)
                undoToolStripMenuItem_Click(sender, e);
        }

        private void redoToolStripMenuItem_code_Click(object sender, EventArgs e)
        {
            if (redoToolStripMenuItem.Enabled)
                redoToolStripMenuItem_Click(sender, e);
        }

        private void cutToolStripMenuItem_code_Click(object sender, EventArgs e)
        {
            if (cutToolStripMenuItem.Enabled)
                cutToolStripMenuItem_Click(sender, e);
        }

        private void copyToolStripMenuItem_code_Click(object sender, EventArgs e)
        {
            if (copyToolStripMenuItem.Enabled)
                copyToolStripMenuItem_Click(sender, e);
        }

        private void pasteToolStripMenuItem_code_Click(object sender, EventArgs e)
        {
            if (pasteToolStripMenuItem.Enabled)
                pasteToolStripMenuItem_Click(sender, e);
        }

        private void deleteToolStripMenuItem_code_Click(object sender, EventArgs e)
        {
            if (deleteToolStripMenuItem.Enabled)
                deleteToolStripMenuItem_Click(sender, e);
        }

        private void selectAllToolStripMenuItem_code_Click(object sender, EventArgs e)
        {
            if (selectAllToolStripMenuItem.Enabled)
                selectAllToolStripMenuItem_Click(sender, e);
        }

        private void codeMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            undoToolStripMenuItem_code.Enabled = undoToolStripMenuItem.Enabled;
            redoToolStripMenuItem_code.Enabled = redoToolStripMenuItem.Enabled;
            cutToolStripMenuItem_code.Enabled = cutToolStripMenuItem.Enabled;
            copyToolStripMenuItem_code.Enabled = copyToolStripMenuItem.Enabled;
            pasteToolStripMenuItem_code.Enabled = pasteToolStripMenuItem.Enabled;
            deleteToolStripMenuItem_code.Enabled = deleteToolStripMenuItem.Enabled;
            selectAllToolStripMenuItem_code.Enabled = selectAllToolStripMenuItem.Enabled;
        }

        private void copyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state.HasFile())
            {
                Clipboard.SetText(state.FileNameString);
                codeBox.Focus();
            }
        }

        private void copyPathToolStripMenuItem_file_Click(object sender, EventArgs e)
        {
            if (state.HasFile())
            {
                Clipboard.SetText(state.FilePathString);
                codeBox.Focus();
            }
        }

        private void showInDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state.HasFile())
            {
                string path = Path.GetDirectoryName(state.FilePathString);
                if (Directory.Exists(path) && File.Exists(state.FilePathString))
                {
                    OpenFolderAndSelectItem(path, state.FilePathString);
                    codeBox.Focus();
                }
            }
        }

        private void OpenFolderAndSelectItem(string folderPath, string filePath)
        {
            IntPtr nativeFolder;
            uint psfgaoOut;
            SHParseDisplayName(folderPath, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);

            if (nativeFolder == IntPtr.Zero)
            {
                return;
            }

            IntPtr nativeFile;
            SHParseDisplayName(filePath, IntPtr.Zero, out nativeFile, 0, out psfgaoOut);

            IntPtr[] fileArray;
            if (nativeFile == IntPtr.Zero)
            {
                fileArray = new IntPtr[0];
            }
            else
            {
                fileArray = new IntPtr[] { nativeFile };
            }

            SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

            Marshal.FreeCoTaskMem(nativeFolder);
            if (nativeFile != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(nativeFile);
            }
        }

        private void copyPathToolStripMenuItem_location_Click(object sender, EventArgs e)
        {
            if (state.HasLocation())
            {
                Clipboard.SetText(state.LocationPathString);
                codeBox.Focus();
            }
        }

        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state.HasLocation())
            {
                enterLocationToolStripMenuItem_Click(sender, e);
                codeBox.Focus();
            }
        }

        private void fileMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            SetNullHint();
            copyPathToolStripMenuItem_file.Enabled = state.HasFile();
            copyNameToolStripMenuItem.Enabled = state.HasFile();
            showInDirectoryToolStripMenuItem.Enabled = state.HasFile() 
                && File.Exists(state.FilePathString);
        }

        private void locationMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            SetNullHint();
            copyPathToolStripMenuItem_location.Enabled = state.HasLocation();
            enterToolStripMenuItem.Enabled = state.HasLocation() 
                && Directory.Exists(state.LocationPathString);
        }

        private void locationBox_MouseDown(object sender, MouseEventArgs e)
        {
            ShowMenuStripOnLeftMouse(locationMenuStrip, locationPanel, e);
        }

        private void fileBox_MouseDown(object sender, MouseEventArgs e)
        {
            ShowMenuStripOnLeftMouse(fileMenuStrip, filePanel, e);
        }

        private void ShowMenuStripOnLeftMouse(ContextMenuStrip strip, Panel referencePanel, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                System.Drawing.Point p = new System.Drawing.Point(referencePanel.Location.X, referencePanel.Location.Y + referencePanel.Size.Height);
                strip.Show(this.PointToScreen(p));
            }
        }


        private void locPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            SetNullHint();
            ShowMenuStripOnLeftMouse(locationMenuStrip, locationPanel, e);
        }

        private void filePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            SetNullHint();
            ShowMenuStripOnLeftMouse(fileMenuStrip, filePanel, e);
        }

        public string GetCode()
        {
            return codeBox.Text;
        }

        public void SetCode(string code)
        {
            LockedTextChange = true;
            codeBox.Text = code;
            LockedTextChange = false;

            History.AddUnit();
            RefreshHistoryMenuItems();
        }

        private void RefreshLineCol()
        {
            int ln = 1;
            int col = 1;

            int start = codeBox.SelectionStart;
            int end = start + codeBox.SelectionLength;
            int limit = MousePressed 
                ? (start == MousePressCaret ? end : start)
                : end;

            string text = codeBox.Text;

            for (int i = 0; i < limit; i++)
            {
                if (text[i].IsNewLine())
                {
                    ln++;
                    col = 1;
                }
                else
                {
                    col++;
                }
            }

            SetLineCol(ln, col);

            if (!MousePressed)
            {
                MousePressed = true;
                MousePressCaret = codeBox.SelectionStart;
            }
        }

        private void SetFinalLineCol()
        {
            int ln = 1;
            int col = 1;
            string text = codeBox.Text;
            int limit = text.Length;

            for (int i = 0; i < limit; i++)
            {
                if (text[i] == '\n')
                {
                    ln++;
                    col = 1;
                }
                else
                {
                    col++;
                }
            }

            SetLineCol(ln, col);
        }

        private void SetLineCol(int line, int col)
        {
            lineLabel.Text = "Ln " + line;
            colLabel.Text = "Col " + col;
        }

        private void codeBox_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;
        }

        private void copyToolStripMenuItem_log_Click(object sender, EventArgs e)
        {
            lock (SyncGate) 
            {
                string text = logBox.Text.Substring(logBox.SelectionStart, logBox.SelectionLength);
                Clipboard.SetText(text);
            }
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (SyncGate)
            {
                Clipboard.SetText(logBox.Text);
            }
        }

        private void FocusOnCodeBox()
        {
            codeBox.Focus();
        }

        private void logBox_MouseDown(object sender, MouseEventArgs e)
        {
            SetNullHint();

            if (logBox.SelectionLength > 0)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                int WM_LBUTTONDOWN = 0x0201;
                int WM_LBUTTONUP = 0x0202;
                int lParam = (e.Y << 16) | (e.X & 0xFFFF);
                SendMessage(logBox.Handle, WM_LBUTTONDOWN, 0x00000001, lParam);
                SendMessage(logBox.Handle, WM_LBUTTONUP, 0x00000000, lParam);
            }
        }
    }
}
