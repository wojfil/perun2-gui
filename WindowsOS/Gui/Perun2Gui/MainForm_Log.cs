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
        }



    }
}
