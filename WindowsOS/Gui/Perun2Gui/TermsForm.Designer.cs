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

namespace Perun2Gui
{
    partial class TermsForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.licenseBox = new System.Windows.Forms.TextBox();
            this.licensePanel = new System.Windows.Forms.Panel();
            this.licensePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.okButton.Location = new System.Drawing.Point(437, 540);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(124, 35);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // licenseBox
            // 
            this.licenseBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.licenseBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.licenseBox.Location = new System.Drawing.Point(4, 4);
            this.licenseBox.Multiline = true;
            this.licenseBox.Name = "licenseBox";
            this.licenseBox.ReadOnly = true;
            this.licenseBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.licenseBox.Size = new System.Drawing.Size(541, 505);
            this.licenseBox.TabIndex = 13;
            // 
            // licensePanel
            // 
            this.licensePanel.BackColor = System.Drawing.Color.White;
            this.licensePanel.Controls.Add(this.licenseBox);
            this.licensePanel.Location = new System.Drawing.Point(12, 12);
            this.licensePanel.Name = "licensePanel";
            this.licensePanel.Size = new System.Drawing.Size(549, 513);
            this.licensePanel.TabIndex = 14;
            // 
            // TermsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Perun2Gui.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(573, 587);
            this.Controls.Add(this.licensePanel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TermsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "License Terms";
            this.Load += new System.EventHandler(this.TermsForm_Load);
            this.licensePanel.ResumeLayout(false);
            this.licensePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox licenseBox;
        private System.Windows.Forms.Panel licensePanel;
    }
}