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
using FastColoredTextBoxNS;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        private bool LockedTextChange;
        private bool CopyEnabled;
        private bool CutEnabled;
        private bool PasteEnabled;
        private bool DeleteEnabled;
        private bool TextOverride;
        private bool SelectAllEnabled;
        private bool CodeAlterEnabled;
        private bool ClearLogsEnabled;
        private bool LowerEnabled;
        private bool UpperEnabled;
        private History History;

        private string ReplaceInput = String.Empty;
        private string ReplaceOutput = String.Empty;

        private bool LogsRecentlyCleaned;
        private int LogLinesCount;

        private void InitHistory()
        {
            History = new History(codeBox);
            RefreshHistoryMenuItems();
            LockedTextChange = false;
            TextOverride = false;
            ClearLogsEnabled = true;
            RefreshPasteEnableness();
            RefreshSelectionMenuItems();
            RefreshSelectAllEnableness();
            RefreshCodeAlterEnableness();
        }

        private void InitMenu()
        {
            enterLocationToolStripMenuItem.Enabled = HasLocation;
        }

        private void RefreshHistoryMenuItems()
        {
            undoToolStripMenuItem.Enabled = !Running && History.HasPrevious();
            redoToolStripMenuItem.Enabled = !Running && History.HasNext();
        }

        private void RefreshSelectionMenuItems()
        {
            RefreshDeleteEnableness();
            RefreshCopyEnableness();
            RefreshCutEnableness();
            RefreshLowerUpperEnableness();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            RefreshPasteEnableness();
            RefreshSelectionMenuItems();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
             LockedTextChange = true;
             History.GoBackward();
             RefreshHistoryMenuItems();
             LockedTextChange = false;
             ScrollToCaret();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
             LockedTextChange = true;
             History.GoForward();
             RefreshHistoryMenuItems();
             LockedTextChange = false;
             ScrollToCaret();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshCutEnableness();
            if (CutEnabled)
            {
                string text = codeBox.Text.Substring(codeBox.SelectionStart, codeBox.SelectionLength);
                Clipboard.SetText(text);
                DeleteSelection();
                ScrollToCaret();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CopyEnabled)
            {
                string text = codeBox.Text.Substring(codeBox.SelectionStart, codeBox.SelectionLength);
                Clipboard.SetText(text);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshPasteEnableness();
            if (PasteEnabled)
            {
                LockedTextChange = true;
                string paste = Clipboard.GetText();
                int index = codeBox.SelectionStart;
                int length = codeBox.SelectionLength;
                string left = codeBox.Text.Substring(0, index);
                string right = codeBox.Text.Substring(index + length);

                codeBox.Text = left + paste + right;
                //codeBox.SelectionStart = index;
                //codeBox.SelectionLength = paste.Length;
                codeBox.SelectionStart = index + paste.Length;
                codeBox.SelectionLength = 0;
                LockedTextChange = false;

                History.AddUnit();
                RefreshHistoryMenuItems();
                ScrollToCaret();
            }
        }

        private void RefreshPasteEnableness()
        {
            string text = Clipboard.GetText();
            PasteEnabled = !Running && text != null && !text.Equals(String.Empty);
            pasteToolStripMenuItem.Enabled = PasteEnabled;
            pasteToolStripMenuItem_code.Enabled = PasteEnabled;
        }

        private void RefreshCopyEnableness()
        {
            CopyEnabled = codeBox.SelectionLength != 0;
            copyToolStripMenuItem.Enabled = CopyEnabled;
            copyToolStripMenuItem_code.Enabled = CopyEnabled;
        }

        private void RefreshCutEnableness()
        {
            CutEnabled = !Running && codeBox.SelectionLength != 0;
            cutToolStripMenuItem.Enabled = CutEnabled;
            cutToolStripMenuItem_code.Enabled = CutEnabled;
        }

        private void RefreshLowerUpperEnableness()
        {
            if (Running || codeBox.SelectionLength == 0)
            {
                LowerEnabled = false;
                UpperEnabled = false;
                return;
            }

            string selection = codeBox.SelectedText;
            int length = selection.Length;
            bool anyLower = false;
            bool anyUpper = false;

            foreach (char ch in selection)
            {
                if (char.IsLower(ch))
                {
                    anyLower = true;
                }
                else if (char.IsUpper(ch))
                {
                    anyUpper = true;
                }

                if (anyLower && anyUpper)
                {
                    break;
                }
            }

            LowerEnabled = anyUpper;
            UpperEnabled = anyLower;
        }

        private void RefreshDeleteEnableness()
        {
            DeleteEnabled = IsDeleteEnabled();
            deleteToolStripMenuItem.Enabled = DeleteEnabled;
            deleteToolStripMenuItem_code.Enabled = DeleteEnabled;
        }

        private bool IsDeleteEnabled()
        {
            if (Running || codeBox.Text.Equals(String.Empty))
                return false;

            if (codeBox.SelectionLength != 0)
                return true;

            return codeBox.SelectionStart != codeBox.Text.Length;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDeleteEnableness();
            if (DeleteEnabled)
            {
                DeleteSelection();
                ScrollToCaret();
            }
        }

        private void DeleteSelection()
        {
            int start = codeBox.SelectionStart;
            int length = codeBox.Text.Length;
            int slength = codeBox.SelectionLength;
            string text = codeBox.Text;
            LockedTextChange = true;

            if (slength == 0)
            {
                if (start == length - 1)
                {
                    codeBox.Text = text.Substring(0, length - 1);
                    codeBox.SelectionStart = length - 1;
                }
                else
                {
                    codeBox.Text = text.Substring(0, start) + text.Substring(start + ((text[start] == '\r') ? 2 : 1 ));
                    codeBox.SelectionStart = start;
                }
            }
            else
            {
                History.SaveCurrentSelection();
                codeBox.Text = text.Substring(0, start) + text.Substring(start + slength);
                codeBox.SelectionStart = start;
                codeBox.SelectionLength = 0;
            }

            LockedTextChange = false;
            History.AddUnit();
            RefreshHistoryMenuItems();
        }

        private void lowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LowerEnabled)
            {
                ChangeCapitalization(true);
            }
        }

        private void upperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UpperEnabled)
            {
                ChangeCapitalization(false);
            }
        }

        private void ChangeCapitalization(bool toLower)
        {
            int start = codeBox.SelectionStart;
            int length = codeBox.Text.Length;
            int slength = codeBox.SelectionLength;
            string text = codeBox.Text;
            string newSelection = toLower 
                ? (codeBox.SelectedText.ToLower()) 
                : (codeBox.SelectedText.ToUpper());

            LockedTextChange = true;
            History.SaveCurrentSelection();
            codeBox.Text = text.Substring(0, start) + newSelection + text.Substring(start + slength);
            codeBox.SelectionStart = start;
            codeBox.SelectionLength = slength;

            LockedTextChange = false;
            History.AddUnit();
            RefreshHistoryMenuItems();
        }

        private void RefreshSelectAllEnableness()
        {
            SelectAllEnabled = !Running && codeBox.Text.Length != 0;
            selectAllToolStripMenuItem.Enabled = SelectAllEnabled;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshSelectAllEnableness();
            if (SelectAllEnabled)
            {
                codeBox.SelectAll();
                codeBox.Focus();
                SetFinalLineCol();
            }
        }

        private void RefreshCodeAlterEnableness()
        {
            CodeAlterEnabled = !Running && codeBox.Text.Length != 0;
            replaceToolStripMenuItem.Enabled = CodeAlterEnabled;
            clearCodeToolStripMenuItem.Enabled = CodeAlterEnabled;
        }

        private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClearLogsEnabled)
            {
                logBox.Clear();
                saveLogsToolStripMenuItem.Enabled = false;
                LogsRecentlyCleaned = true;
                LogLinesCount = 0;
            }
        }

        private void clearCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CodeAlterEnabled)
            {
                History.SaveCurrentSelection();
                codeBox.Clear();
            }
        }

        private void EnableClearLogs()
        {
            clearLogsToolStripMenuItem.Enabled = true;
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ReplaceForm(this, ReplaceInput, ReplaceOutput);
            form.ShowDialog();
            ReplaceInput = form.GetInputText();
            ReplaceOutput = form.GetOutputText();
        }

        private void ScrollToCaret()
        {
            if (codeBox.SelectionLength == 0)
            {
                ScrollToCaretSimple();
            }
            else
            {
                ScrollToCaretSelection();
            }
        }

        private void ScrollToCaretSimple()
        {
            bool hasVertical = codeBox.VerticalScroll.Maximum >= codeBox.Height;
            bool hasHorizontal = codeBox.HorizontalScroll.Maximum >= codeBox.Width;

            if (!hasVertical && !hasHorizontal)
            {
                return;
            }

            string beforeCaret = codeBox.Text.Substring(0, codeBox.SelectionStart);
            int lineId = beforeCaret.LastIndexOf('\n');
            string lastLine = lineId == -1 ? beforeCaret : beforeCaret.Substring(lineId + 1);

            var beforeCaretSize = TextRenderer.MeasureText(beforeCaret, codeBox.Font);
            var lastLineSize = TextRenderer.MeasureText(lastLine, codeBox.Font);
            var totalSize = TextRenderer.MeasureText(codeBox.Text, codeBox.Font);

            int x = lastLineSize.Width - codeBox.Width / 2;
            int y = beforeCaretSize.Height - codeBox.Height / 2;

            if (hasHorizontal)
            {
                if (x < 0 || totalSize.Width < codeBox.Width)
                {
                    codeBox.HorizontalScroll.Value = 0;
                }
                else
                {
                    codeBox.HorizontalScroll.Value = x;
                }
            }

            if (hasVertical)
            {
                if (y < 0 || totalSize.Height < codeBox.Height)
                {
                    codeBox.VerticalScroll.Value = 0;
                }
                else
                {
                    codeBox.VerticalScroll.Value = y;
                }
            }

            codeBox.UpdateScrollbars();
        }

        private void ScrollToCaretSelection()
        {

            bool hasVertical = codeBox.VerticalScroll.Maximum >= codeBox.Height;
            bool hasHorizontal = codeBox.HorizontalScroll.Maximum >= codeBox.Width;

            if (!hasVertical && !hasHorizontal)
            {
                return;
            }

            int caretId = codeBox.SelectionStart + codeBox.SelectionLength;
            string beforeCaret = codeBox.Text.Substring(0, caretId);
            int lineId = beforeCaret.LastIndexOf('\n');
            string lastLine = lineId == -1 ? beforeCaret : beforeCaret.Substring(lineId + 1);

            var beforeCaretSize = TextRenderer.MeasureText(beforeCaret, codeBox.Font);
            var lastLineSize = TextRenderer.MeasureText(lastLine, codeBox.Font);
            var totalSize = TextRenderer.MeasureText(codeBox.Text, codeBox.Font);

            int x = lastLineSize.Width - codeBox.Width / 2;
            int y = beforeCaretSize.Height - codeBox.Height / 2;

            if (hasHorizontal)
            {
                if (x < 0|| totalSize.Width < codeBox.Width)
                {
                    codeBox.HorizontalScroll.Value = 0;
                }
                else
                {
                    codeBox.HorizontalScroll.Value = x;
                }
            }

            if (hasVertical)
            {
                if (y < 0 || totalSize.Height < codeBox.Height)
                {
                    codeBox.VerticalScroll.Value = 0;
                }
                else
                {
                    codeBox.VerticalScroll.Value = y;
                }
            }

            codeBox.UpdateScrollbars();
        }
    }
}
