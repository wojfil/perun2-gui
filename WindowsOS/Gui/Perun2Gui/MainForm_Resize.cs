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
using System.Drawing;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        private const int MIN_WIDTH = 887;
        private const int MIN_HEIGHT = 507;
        private static readonly Size MIN_SIZE = new Size(MIN_WIDTH, MIN_HEIGHT);
        private static readonly Size MAX_SIZE = new Size(0, 0);
        private Rectangle WholeScreen;
        private const int LOG_LINES_HEAVY = 17000;
        private bool ResizeEnabled;
        private FormWindowState PrevWindowState;



        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (ResizeEnabled)
            {
                codeBox.Height = this.Height - 154 + 0;
                codeBox.Width = this.Width - 396;
                logPanel.Left = this.Width - 372;

                if (LogLinesCount < LOG_LINES_HEAVY)
                {
                    logPanel.Height = this.Height - 154 + 0;
                    logBox.Height = this.Height - 160 - 3;
                }
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            TryToResizeLogBox();
        }

        private void TryToResizeLogBox()
        {
            if (LogLinesCount >= LOG_LINES_HEAVY)
            {
                logPanel.Height = this.Height - 154 + 0;
                logBox.Height = this.Height - 160 - 3;
            }
        }

        private void FitScreen()
        {
            string code = codeBox.Text;
            var size = TextRenderer.MeasureText(code, codeBox.Font);

            int w = 445 + size.Width;
            int h = 156 + size.Height;

            if (w < this.MinimumSize.Width)
            {
                w = this.MinimumSize.Width;
            }

            if (h < this.MinimumSize.Height)
            {
                h = this.MinimumSize.Height;
            }

            int l = this.Left > 0 ? this.Left : 0;
            int t = this.Top > 0 ? this.Top : 0;
            bool fullWidth = false;
            bool fullHeight = false;

            if (l + w > WholeScreen.Width)
            {
                l = WholeScreen.Width - w;
                if (l < 0)
                {
                    fullWidth = true;
                    l = 0;
                }
            }

            if (t + h > WholeScreen.Height)
            {
                t = WholeScreen.Height - h;
                if (t < 0)
                {
                    fullHeight = true;
                    t = 0;
                }
            }

            if (w > WholeScreen.Width)
            {
                w = WholeScreen.Width;
            }

            if (h > WholeScreen.Height)
            {
                h = WholeScreen.Height;
            }

            bool normal = this.WindowState == FormWindowState.Normal;
            this.WindowState = FormWindowState.Normal;
            this.Left = l;
            this.Top = t;
            this.Width = w;
            this.Height = h;

            if (fullWidth || fullHeight)
            {
                SetFullScreen();
            }
            else if (!normal)
            {
                this.CenterToScreen();
            }

            TryToResizeLogBox();
        }

        private void EnableResizeByUser()
        {
            ResizeEnabled = true;
            this.MaximumSize = MAX_SIZE;
            this.MinimumSize = MIN_SIZE;
        }

        private void DisableResizeByUser()
        {
            ResizeEnabled = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void SmallScreen()
        {
            if (this.Width == MIN_WIDTH && this.Height == MIN_HEIGHT && this.WindowState == FormWindowState.Normal)
            {
                // the window is already small
                MoveWindowBackToScreen();
                return;
            }

            int widthExcess = this.Width - MIN_WIDTH;
            int heightExcess = this.Height - MIN_HEIGHT;

            this.Width -= widthExcess;
            this.Height -= heightExcess;

            if (this.WindowState != FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Normal;
                this.CenterToScreen();
            }
            else
            {
                MoveWindowBackToScreen();
            }

            TryToResizeLogBox();
        }

        private void MoveWindowBackToScreen()
        {
            if (this.Left < 0)
                this.Left = 0;

            if (this.Top < 0)
                this.Top = 0;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != PrevWindowState)
            {
                if (this.WindowState != FormWindowState.Minimized && PrevWindowState != FormWindowState.Minimized)
                {
                    TryToResizeLogBox();
                }

                PrevWindowState = this.WindowState;
            }
        }

        private void TryWindowWidening()
        {
            if (this.Width > WholeScreen.Width)
            {
                this.Left = 0;
                return;
            }

            var textSize = TextRenderer.MeasureText(codeBox.Text, codeBox.Font);
            var currentWidth = this.Width;

            int w = 445 + textSize.Width;
            if (w > WholeScreen.Width)
            {
                this.Left = 0;
                this.Width = WholeScreen.Width;
                return;
            }

            var a = currentWidth - textSize.Width - 435;
            if (a < 0)
            {
                var newWidth = this.Width - a;

                if (this.Left < 0)
                {
                    this.Left = 0;
                }
                if (this.Top < 0)
                {
                    this.Top = 0;
                }

                if (this.Left + newWidth > WholeScreen.Width)
                {
                    this.Left = WholeScreen.Width - newWidth;
                }

                this.Width = newWidth;
            }
        }
    }
}
