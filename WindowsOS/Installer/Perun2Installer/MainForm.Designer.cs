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

namespace Perun2Installer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.nextButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.panelHead = new System.Windows.Forms.Panel();
            this.panelFinish = new System.Windows.Forms.Panel();
            this.openPerun2Box = new System.Windows.Forms.CheckBox();
            this.panelFailed = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.menuStartShortcutBox = new System.Windows.Forms.CheckBox();
            this.desktopShortcutBox = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelHead1 = new System.Windows.Forms.Label();
            this.panelRequirements = new System.Windows.Forms.Panel();
            this.panelLicense = new System.Windows.Forms.Panel();
            this.panelInstallation = new System.Windows.Forms.Panel();
            this.panelPath = new System.Windows.Forms.Panel();
            this.selectLocationButton = new System.Windows.Forms.Button();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.pathSizeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.installationProgressBar = new System.Windows.Forms.ProgressBar();
            this.label10 = new System.Windows.Forms.Label();
            this.licenseCheckBox = new System.Windows.Forms.CheckBox();
            this.licenseBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelRequirements = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.topStripPanel = new System.Windows.Forms.Panel();
            this.topStripPanel2 = new System.Windows.Forms.Panel();
            this.logoBox_head = new System.Windows.Forms.PictureBox();
            this.topStripPanel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.recommendedLabel = new System.Windows.Forms.Label();
            this.panelHead.SuspendLayout();
            this.panelFinish.SuspendLayout();
            this.panelFailed.SuspendLayout();
            this.panelRequirements.SuspendLayout();
            this.panelLicense.SuspendLayout();
            this.panelInstallation.SuspendLayout();
            this.panelPath.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox_head)).BeginInit();
            this.SuspendLayout();
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.SystemColors.Control;
            this.nextButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.nextButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.nextButton, "nextButton");
            this.nextButton.Name = "nextButton";
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // backButton
            // 
            resources.ApplyResources(this.backButton, "backButton");
            this.backButton.Name = "backButton";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // panelHead
            // 
            this.panelHead.Controls.Add(this.panelFinish);
            this.panelHead.Controls.Add(this.labelHead1);
            this.panelHead.Controls.Add(this.label1);
            resources.ApplyResources(this.panelHead, "panelHead");
            this.panelHead.Name = "panelHead";
            // 
            // panelFinish
            // 
            this.panelFinish.Controls.Add(this.openPerun2Box);
            this.panelFinish.Controls.Add(this.panelFailed);
            this.panelFinish.Controls.Add(this.panelRequirements);
            this.panelFinish.Controls.Add(this.menuStartShortcutBox);
            this.panelFinish.Controls.Add(this.desktopShortcutBox);
            this.panelFinish.Controls.Add(this.label12);
            resources.ApplyResources(this.panelFinish, "panelFinish");
            this.panelFinish.Name = "panelFinish";
            // 
            // openPerun2Box
            // 
            resources.ApplyResources(this.openPerun2Box, "openPerun2Box");
            this.openPerun2Box.Checked = true;
            this.openPerun2Box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.openPerun2Box.Name = "openPerun2Box";
            this.openPerun2Box.UseVisualStyleBackColor = true;
            // 
            // panelFailed
            // 
            this.panelFailed.Controls.Add(this.label11);
            resources.ApplyResources(this.panelFailed, "panelFailed");
            this.panelFailed.Name = "panelFailed";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // menuStartShortcutBox
            // 
            resources.ApplyResources(this.menuStartShortcutBox, "menuStartShortcutBox");
            this.menuStartShortcutBox.Checked = true;
            this.menuStartShortcutBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuStartShortcutBox.Name = "menuStartShortcutBox";
            this.menuStartShortcutBox.UseVisualStyleBackColor = true;
            // 
            // desktopShortcutBox
            // 
            resources.ApplyResources(this.desktopShortcutBox, "desktopShortcutBox");
            this.desktopShortcutBox.Name = "desktopShortcutBox";
            this.desktopShortcutBox.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // labelHead1
            // 
            resources.ApplyResources(this.labelHead1, "labelHead1");
            this.labelHead1.Name = "labelHead1";
            // 
            // panelRequirements
            // 
            this.panelRequirements.Controls.Add(this.recommendedLabel);
            this.panelRequirements.Controls.Add(this.label5);
            this.panelRequirements.Controls.Add(this.labelRequirements);
            this.panelRequirements.Controls.Add(this.label3);
            resources.ApplyResources(this.panelRequirements, "panelRequirements");
            this.panelRequirements.Name = "panelRequirements";
            // 
            // panelLicense
            // 
            this.panelLicense.Controls.Add(this.panelInstallation);
            this.panelLicense.Controls.Add(this.licenseCheckBox);
            this.panelLicense.Controls.Add(this.licenseBox);
            this.panelLicense.Controls.Add(this.label4);
            resources.ApplyResources(this.panelLicense, "panelLicense");
            this.panelLicense.Name = "panelLicense";
            // 
            // panelInstallation
            // 
            this.panelInstallation.Controls.Add(this.panelPath);
            this.panelInstallation.Controls.Add(this.label9);
            this.panelInstallation.Controls.Add(this.installationProgressBar);
            this.panelInstallation.Controls.Add(this.label10);
            resources.ApplyResources(this.panelInstallation, "panelInstallation");
            this.panelInstallation.Name = "panelInstallation";
            // 
            // panelPath
            // 
            this.panelPath.Controls.Add(this.selectLocationButton);
            this.panelPath.Controls.Add(this.pathBox);
            this.panelPath.Controls.Add(this.pathSizeLabel);
            this.panelPath.Controls.Add(this.label2);
            resources.ApplyResources(this.panelPath, "panelPath");
            this.panelPath.Name = "panelPath";
            // 
            // selectLocationButton
            // 
            this.selectLocationButton.BackColor = System.Drawing.SystemColors.Control;
            this.selectLocationButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.selectLocationButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.selectLocationButton, "selectLocationButton");
            this.selectLocationButton.Name = "selectLocationButton";
            this.selectLocationButton.UseVisualStyleBackColor = false;
            this.selectLocationButton.Click += new System.EventHandler(this.selectLocationButton_Click);
            // 
            // pathBox
            // 
            resources.ApplyResources(this.pathBox, "pathBox");
            this.pathBox.Name = "pathBox";
            this.pathBox.ReadOnly = true;
            // 
            // pathSizeLabel
            // 
            resources.ApplyResources(this.pathSizeLabel, "pathSizeLabel");
            this.pathSizeLabel.Name = "pathSizeLabel";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // installationProgressBar
            // 
            resources.ApplyResources(this.installationProgressBar, "installationProgressBar");
            this.installationProgressBar.Name = "installationProgressBar";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // licenseCheckBox
            // 
            resources.ApplyResources(this.licenseCheckBox, "licenseCheckBox");
            this.licenseCheckBox.Name = "licenseCheckBox";
            this.licenseCheckBox.UseVisualStyleBackColor = true;
            this.licenseCheckBox.CheckedChanged += new System.EventHandler(this.licenseCheckBox_CheckedChanged);
            // 
            // licenseBox
            // 
            resources.ApplyResources(this.licenseBox, "licenseBox");
            this.licenseBox.Name = "licenseBox";
            this.licenseBox.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // labelRequirements
            // 
            resources.ApplyResources(this.labelRequirements, "labelRequirements");
            this.labelRequirements.Name = "labelRequirements";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.SystemColors.Control;
            this.bottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomPanel.Controls.Add(this.backButton);
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Controls.Add(this.nextButton);
            resources.ApplyResources(this.bottomPanel, "bottomPanel");
            this.bottomPanel.Name = "bottomPanel";
            // 
            // topStripPanel
            // 
            this.topStripPanel.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.topStripPanel, "topStripPanel");
            this.topStripPanel.Name = "topStripPanel";
            // 
            // topStripPanel2
            // 
            this.topStripPanel2.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.topStripPanel2, "topStripPanel2");
            this.topStripPanel2.Name = "topStripPanel2";
            // 
            // logoBox_head
            // 
            this.logoBox_head.Image = global::Perun2Installer.Properties.Resources.perunlogo;
            resources.ApplyResources(this.logoBox_head, "logoBox_head");
            this.logoBox_head.Name = "logoBox_head";
            this.logoBox_head.TabStop = false;
            // 
            // topStripPanel3
            // 
            this.topStripPanel3.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.topStripPanel3, "topStripPanel3");
            this.topStripPanel3.Name = "topStripPanel3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // recommendedLabel
            // 
            resources.ApplyResources(this.recommendedLabel, "recommendedLabel");
            this.recommendedLabel.Name = "recommendedLabel";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelLicense);
            this.Controls.Add(this.topStripPanel3);
            this.Controls.Add(this.topStripPanel2);
            this.Controls.Add(this.topStripPanel);
            this.Controls.Add(this.logoBox_head);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.panelHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.panelHead.ResumeLayout(false);
            this.panelHead.PerformLayout();
            this.panelFinish.ResumeLayout(false);
            this.panelFinish.PerformLayout();
            this.panelFailed.ResumeLayout(false);
            this.panelFailed.PerformLayout();
            this.panelRequirements.ResumeLayout(false);
            this.panelRequirements.PerformLayout();
            this.panelLicense.ResumeLayout(false);
            this.panelLicense.PerformLayout();
            this.panelInstallation.ResumeLayout(false);
            this.panelInstallation.PerformLayout();
            this.panelPath.ResumeLayout(false);
            this.panelPath.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox_head)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Panel panelHead;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel topStripPanel;
        private System.Windows.Forms.Panel topStripPanel2;
        private System.Windows.Forms.PictureBox logoBox_head;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelHead1;
        private System.Windows.Forms.Panel panelRequirements;
        private System.Windows.Forms.Label labelRequirements;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelLicense;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox licenseBox;
        private System.Windows.Forms.CheckBox licenseCheckBox;
        private System.Windows.Forms.Panel panelPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label pathSizeLabel;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Button selectLocationButton;
        private System.Windows.Forms.Panel topStripPanel3;
        private System.Windows.Forms.Panel panelInstallation;
        private System.Windows.Forms.ProgressBar installationProgressBar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panelFinish;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox menuStartShortcutBox;
        private System.Windows.Forms.CheckBox desktopShortcutBox;
        private System.Windows.Forms.Panel panelFailed;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox openPerun2Box;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label recommendedLabel;
    }
}

