using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {

        private void logMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            copyToolStripMenuItem_log.Enabled = ResizeEnabled && logBox.SelectionLength != 0;
            copyAllToolStripMenuItem.Enabled = ResizeEnabled && logBox.Text.Any();

            RefreshLogMenuStripState();
        }

        string TrimmedSelectedLine(string line)
        {
            string trimmed = line.Trim();

            int quoteCount = trimmed.Count(c => c == '"');

            if (quoteCount == 1)
            {
                return String.Empty;
            }

            if (quoteCount == 2)
            {
                string after = trimmed.Substring(trimmed.IndexOf('"') + 1);
                trimmed = after.Substring(0, after.IndexOf('"')).Trim();
            }

            return trimmed;
        }
        bool IsNewLine(char c)
        {
            return c == '\n' || c == '\r';
        }

        string[] SelectedLogLines()
        {
            int selectionStart = logBox.SelectionStart;
            int selectionEnd = selectionStart + logBox.SelectionLength;

            while (selectionStart >= 0)
            {
                if (IsNewLine(logBox.Text[selectionStart]))
                {
                    break;
                }

                selectionStart--;
            }

            while (selectionEnd < logBox.Text.Length)
            {
                if (IsNewLine(logBox.Text[selectionEnd]))
                {
                    break;
                }

                selectionEnd++;
            }

            string selectedText = logBox.Text.Substring(selectionStart, selectionEnd - selectionStart);
            return selectedText.Split(new[] { "\r\n", "\n" },
                StringSplitOptions.None); ;
        }

        List<string> ExistingSelectedLogLines(bool forSelection)
        {
            string[] lines = SelectedLogLines();

            List<string> result = new List<string>();

            foreach (string line in lines)
            {
                string trimmed = TrimmedSelectedLine(line);

                if (trimmed.Count() == 0)
                {
                    continue;
                }

                try
                {
                    string path = Path.GetFullPath(Path.Combine(LocationString, trimmed));

                    if (!File.Exists(path) && !Directory.Exists(path))
                    {
                        continue;
                    }

                    if (forSelection && ! IsSelectable(path)) {
                        continue;
                    }

                    result.Add(Path.Combine(LocationString, trimmed));
                }
                catch { }
            }

            return result;
        }

        private bool IsSelectable(string path)
        {
            string trimmed = path.TrimEnd('\\').TrimEnd('/');

            if (trimmed.Length == 2 && trimmed[1] == ':')
            {
                return false;
            }

            return true;
        }

        private void RefreshLogMenuStripState()
        {
            if (! HasLocation)
            {
                showThemToolStripMenuItem.Enabled = false;
                openThemToolStripMenuItem.Enabled = false;
                return;
            }

            bool knownShow = false;
            bool knownOpen = false;

            string[] lines = SelectedLogLines();

            foreach (string line in lines)
            {
                if (knownShow && knownOpen)
                {
                    return;
                }

                string trimmed = TrimmedSelectedLine(line);

                if (trimmed.Count() == 0)
                {
                    continue;
                }

                try
                {
                    string path = Path.GetFullPath(Path.Combine(LocationString, trimmed));

                    if (! File.Exists(path) && ! Directory.Exists(path))
                    {
                        continue;
                    }

                    if (! knownOpen)
                    {
                        openThemToolStripMenuItem.Enabled = true;
                        knownOpen = true;
                    }

                    if (! knownShow)
                    {
                        if (IsSelectable(path))
                        {
                            showThemToolStripMenuItem.Enabled = true;
                            knownShow = true;
                        }
                    }
                } catch { }
            }

            if (! knownShow)
            {
                showThemToolStripMenuItem.Enabled = false;
            }

            if (! knownOpen)
            {
                openThemToolStripMenuItem.Enabled = false;
            }
        }

        private void showThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> paths = ExistingSelectedLogLines(true);
            Explorer.SelectFiles(paths);
        }

        private void openThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> paths = ExistingSelectedLogLines(false);

            if (paths.Count >= Constants.A_LOT_TO_OPEN_THREHOLD)
            {
                var form = new OpenConfirmForm(paths.Count);
                form.ShowDialog();

                if (form.DialogResult != DialogResult.OK)
                {
                    return;
                }
            }

            foreach (string path in paths)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex) { }
            }
        }
    }
}
