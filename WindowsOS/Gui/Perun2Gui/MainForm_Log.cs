using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {

        private void logMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            copyToolStripMenuItem_log.Enabled = ResizeEnabled && logBox.SelectionLength != 0;
            copyAllToolStripMenuItem.Enabled = ResizeEnabled && logBox.Text.Any();

            LogsSelectionState state = GetLogSelectionState();

            switch (state)
            {
                case LogsSelectionState.None: 
                {
                    showThemToolStripMenuItem.Text = "Show them in Explorer";
                    openThemToolStripMenuItem.Text = "Open them";
                    showThemToolStripMenuItem.Enabled = false;
                    openThemToolStripMenuItem.Enabled = false;
                    break;
                }
                case LogsSelectionState.OneFile:
                {
                    showThemToolStripMenuItem.Text = "Show it in Explorer";
                    openThemToolStripMenuItem.Text = "Open it";
                    showThemToolStripMenuItem.Enabled = true;
                    openThemToolStripMenuItem.Enabled = true;
                    break;
                }
                case LogsSelectionState.ManyFiles:
                {
                    showThemToolStripMenuItem.Text = "Show them in Explorer";
                    openThemToolStripMenuItem.Text = "Open them";
                    showThemToolStripMenuItem.Enabled = true;
                    openThemToolStripMenuItem.Enabled = true;
                    break;
                }
            }
        }

        string[] GetSelectedLogLines()
        {
            return logBox.SelectedText.Split(new[] { "\r\n", "\n" }, 
                StringSplitOptions.None);
        }

        private LogsSelectionState GetLogSelectionState()
        {
            if (logBox.SelectionLength == 0)
            {
                return LogsSelectionState.None;
            }

            string[] lines = GetSelectedLogLines();

            return lines.Length == 1
                ? LogsSelectionState.OneFile
                : LogsSelectionState.ManyFiles;
        }

        private void showThemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new OpenConfirmForm(14);
            form.ShowDialog();

            if (form.DialogResult != DialogResult.OK)
            {
                return;
            }

            
        }
    }
}
