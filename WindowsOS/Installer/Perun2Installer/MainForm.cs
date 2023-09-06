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
using Perun2Installer.Actions;

namespace Perun2Installer
{
    public partial class MainForm : Form
    {
        private PageManager PageManager;
        private string LicenseKey;
        Actions.ActionChain installationActions = new Actions.ActionChain();
        private bool AlreadyInstalled = false;

        public MainForm()
        {
            InitializeComponent();
            InitControls();
            InitPanels();
            SetDefaultLicense();
            SetDefaultInstallationPath();
            SetFirstTexts();
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
        }

        private bool OutdatedOS()
        {
            var v = System.Environment.OSVersion;

            if (v.Version.Major < 6)
            {
                return true;
            }

            return v.Version.Major == 6 && v.Version.Minor == 0;
        }

        private void SetFirstTexts()
        {
            if (OutdatedOS())
            {
                welcomeLabel.Text = "Your operating system is outdated!";
                labelHead1.Text = "At least " + Constants.RECOMMENDED_SYSTEM + " is required.";
                nextButton.Enabled = false;
                cancelButton.Text = "Finish";
                return;
            }

            long space = GetDiscSpace();
            labelHead1.Text = "Follow these steps to install\nPerun2 " + GetVersionString() + " on your machine.";
            labelRequirements.Text = "- " + Constants.RECOMMENDED_SYSTEM + " or newer" + Environment.NewLine +
                                     "- free disc space " + SpaceToString(space);

            recommendedLabel.Text = "- free disc space at least " + GetRecommendedSpace(space) + " MB";
            pathSizeLabel.Text = "Required space: " + SpaceToString(space);
            licenseBox.Text = Resources.LICENSE;

            AlreadyInstalled = IsAlreadyInstalled();

            if (AlreadyInstalled)
            {
                welcomeLabel.Text = "Actualization";
                labelHead1.Text = "Follow these steps to update\nPerun2 to version " + GetVersionString() + ".";
                this.Text = "Perun2 Actualization";

                string path = GetInstallDirectory();
                if (!path.Equals(""))
                {
                    installationActions.isActualization = true;
                    installationActions.InitActualization();
                    SetInstallationPath(path);
                }

                successTextLabel.Text = "Perun2 has been actualized sucessfully.";
                menuStartShortcutBox.Checked = false;
                menuStartShortcutBox.Visible = false;
                desktopShortcutBox.Checked = false;
                desktopShortcutBox.Visible = false;
            }
        }

        private static bool IsAlreadyInstalled()
        {
            return RegistryAction.KeyExistsOnLocalMachine(Actions.Registry_Uninstaller.UninstallRegistry)
                || RegistryAction.KeyExistsOnLocalMachine(Actions.Registry_Uninstaller.UninstallRegistry32on64);
        }
        private static string GetInstallDirectory()
        {
            if (RegistryAction.KeyExistsOnLocalMachine(Actions.Registry_Uninstaller.UninstallRegistry))
            {
                return RegistryAction.GetInstallationPath(Actions.Registry_Uninstaller.UninstallRegistry);
            }
            else
            {
                return RegistryAction.GetInstallationPath(Actions.Registry_Uninstaller.UninstallRegistry32on64);
            }
        }

        public static string GetVersionString()
        {
            return Application.ProductVersion.Substring(0, Application.ProductVersion.Length - 2);
        }

        private string SpaceToString(double space)
        {
            double mb = (double)space / (1024d * 1024d);
            long units = (long)Math.Ceiling(mb * 100d);

            long upper = units / 100;
            long lower = units % 100;

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
            Panel panel = PageManager.GetCurrentPanel();

            if (PageManager.HasBackPage())
            {
                if (AlreadyInstalled && panel == panelLicense)
                {
                    PageManager.Back();
                }
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

                if (AlreadyInstalled && panel == panelHead)
                {
                    PageManager.Next();
                }

                PageEntered();
            }

            if (AlreadyInstalled && panel == panelRequirements)
            {
                nextButton.Text = GetFinalButtonText();
            }
            else if (AlreadyInstalled && panel == panelLicense)
            {
                PageManager.Next();
                StartInstall();
            }
            else if (panel == panelPath)
            {
                StartInstall();
            }
        }

        private string GetFinalButtonText()
        {
            return AlreadyInstalled ? "Actualize" : "Install";
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

            if (AlreadyInstalled)
            {
                nextButton.Text = (panel == panelLicense) ? "Actualize" : "Next";
            }
            else
            {
                nextButton.Text = (panel == panelPath) ? "Install" : "Next";
            }
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
