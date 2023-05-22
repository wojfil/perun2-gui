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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Perun2Installer.Properties;
using System.Threading;
using Microsoft.Win32;
using System.Reflection;
using System.IO;

namespace Perun2Installer
{
    public partial class MainForm : Form
    {
        private PageManager PageManager;
        private string LicenseKey;
        Actions.ActionChain installationActions = new Actions.ActionChain();

        public MainForm()
        {
            InitializeComponent();
            InitControls();
            InitPanels();
            SetDefaultLicense();
            SetDefaultInstallationPath();
        }

        private void InitPanels()
        {
            PageManager = new PageManager(this);

            PageManager.AddPanel(panelHead, true);
            PageManager.AddPanel(panelRequirements, true);
            PageManager.AddPanel(panelLicense, true);
            PageManager.AddPanel(panelPath, true);
            PageManager.AddPanel(panelInstallation, false);
            PageManager.AddPanel(panelFinish, false);
            PageManager.AddPanel(panelFailed, false);

            PageManager.Lock();
            PageManager.RefreshVisibility();
        }

        private void InitControls()
        {
            this.Icon = Resources.perun256;

            topStripPanel.BackColor = Constants.TOP_STRIP_COLOR;
            topStripPanel2.BackColor = Constants.TOP_STRIP_COLOR_2;
            topStripPanel3.BackColor = Constants.TOP_STRIP_COLOR_3;

            topStripPanel.Location = new Point(0, 0);
            topStripPanel2.Location = new Point(0, Constants.TOP_STRIP_HEIGHT);
            topStripPanel3.Location = new Point(0, 2 * Constants.TOP_STRIP_HEIGHT);

            topStripPanel.Size = new Size(Constants.FORM_WIDTH, Constants.TOP_STRIP_HEIGHT);
            topStripPanel2.Size = new Size(Constants.FORM_WIDTH, Constants.TOP_STRIP_HEIGHT);
            topStripPanel3.Size = new Size(Constants.FORM_WIDTH, Constants.TOP_STRIP_HEIGHT);

            long space = GetDiscSpace();

            labelHead1.Text = "Follow these steps to install\nPerun2 " + GetVersionString() + " on your machine.";
            labelRequirements.Text = "- " + Constants.RECOMMENDED_SYSTEM + " or newer" + Environment.NewLine +
                                     "- free disc space " + SpaceToString(space);

            recommendedLabel.Text = "- free disc space at least " + GetRecommendedSpace(space) + " MB";

            pathSizeLabel.Text = "Required space: " + SpaceToString(space);

            licenseBox.Text = Resources.LICENSE;
        }

        public static string GetVersionString()
        {
            return Application.ProductVersion.Substring(0, Application.ProductVersion.Length - 2);
        }

        private string SpaceToString(double space)
        {
            double mb = (double)space / (1024d * 1024d);
            int units = (int)Math.Ceiling(mb * 100d);

            int upper = units / 100;
            int lower = units % 100;

            return upper + "." + lower + " MB";
        }

        private long GetDiscSpace()
        {
            return Properties.Resources.perun2.LongLength
                + Properties.Resources.Perun2Gui.LongLength
                + Properties.Resources.Perun2Manager.LongLength
                + Properties.Resources.uninstall.LongLength
                + Properties.Resources.Delete_empty_directories.LongLength
                + Properties.Resources.Select_all.LongLength;
        }

        private long GetRecommendedSpace(long space)
        {
            double mb = (double)space / (1024d * 1024d);
            return (long)Math.Ceiling(mb * 1.5d);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (PageManager.HasBackPage())
            {
                PageManager.Back();
                PageEntered();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            Panel panel = PageManager.GetCurrentPanel();

            if (panel == panelFinish)
            {
                Finish();
                return;
            }
            else if (panel == panelFailed)
            {
                Application.Exit();
                return;
            }

            if (PageManager.HasNextPage())
            {
                PageManager.Next();
                PageEntered();
            }


            if (panel == panelPath)
            {
                StartInstall();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PageEntered()
        {
            Panel panel = PageManager.GetCurrentPanel();

            if (panel == panelLicense)
            {
                if (nextButton.Enabled)
                {
                    if (!licenseCheckBox.Checked)
                    {
                        nextButton.Enabled = false;
                    }
                }
            }
            else if (panel == panelPath)
            {
                bool hasPath = Paths.GetInstance().HasPath();

                if (hasPath)
                {
                    CheckInstallationPathExistence();
                }
                nextButton.Enabled = hasPath;
            }

            nextButton.Text = (panel == panelPath) ? "Install" : "Next";

        }

        private void licenseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            nextButton.Enabled = licenseCheckBox.Checked;
        }

        private void selectLocationButton_Click(object sender, EventArgs e)
        {
            LoadInstallationPath();
        }

        public Button GetBackButton()
        {
            return backButton;
        }

        public Button GetNextButton()
        {
            return nextButton;
        }
    }
}
