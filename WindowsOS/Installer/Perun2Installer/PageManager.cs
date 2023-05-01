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

namespace Perun2Installer
{
    public class PageManager
    {
        private bool Locked;
        private int Index;
        private List<bool> Reversibles;
        private List<Panel> Panels;
        private MainForm MainForm;
        private Button BackButton;
        private Button NextButton;


        public PageManager(MainForm mainForm)
        {
            MainForm = mainForm;
            BackButton = mainForm.GetBackButton();
            NextButton = mainForm.GetNextButton();
            Index = 0;
            Locked = false;
            Panels = new List<Panel>();
            Reversibles = new List<bool>();
        }

        public void AddPanel(Panel panel, bool isReversible)
        {
            if (!Locked)
            {
                InitPanel(panel);
                Panels.Add(panel);
                Reversibles.Add(isReversible);
            }
        }

        private void InitPanel(Panel panel)
        {
            panel.Parent = MainForm;
            panel.Location = new Point(Constants.PANEL_START_X, Constants.TOP_STRIPS * Constants.TOP_STRIP_HEIGHT);
        }

        public void Lock()
        {
            Locked = true;
        }

        public bool HasNextPage()
        {
            return Index != Panels.Count() - 1;
        }

        public bool HasBackPage()
        {
            return Index != 0 && Reversibles[Index];
        }

        public void Next()
        {
            Index++;
            RefreshVisibility();
        }

        public void Back()
        {
            Index--;
            RefreshVisibility();
        }

        public void RefreshVisibility()
        {
            int length = Panels.Count();
            for (int i = 0; i < length; i++)
            {
                Panel p = Panels[i];
                if (i == Index)
                {
                    p.Visible = true;
                    p.Show();
                }
                else
                {
                    p.Visible = false;
                    p.Hide();
                }
            }

            BackButton.Enabled = HasBackPage();
            NextButton.Enabled = HasNextPage();
        }

        public Panel GetCurrentPanel()
        {
            return Panels[Index];
        }

        public List<Panel> GetAllPanels()
        {
            return Panels;
        }

        public void EnterLastPage()
        {
            Index = Panels.Count() - 1;
            RefreshVisibility();
        }
    }
}
