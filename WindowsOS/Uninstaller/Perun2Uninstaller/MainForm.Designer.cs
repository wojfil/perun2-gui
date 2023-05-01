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

namespace Perun2Uninstaller
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.topStripPanel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.topStripPanel2 = new System.Windows.Forms.Panel();
            this.topStripPanel3 = new System.Windows.Forms.Panel();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.uninstallButton);
            this.panel1.Controls.Add(this.backButton);
            this.panel1.Location = new System.Drawing.Point(-5, 288);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 64);
            this.panel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cancelButton.Location = new System.Drawing.Point(492, 11);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(120, 30);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // uninstallButton
            // 
            this.uninstallButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uninstallButton.Location = new System.Drawing.Point(323, 11);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(120, 30);
            this.uninstallButton.TabIndex = 1;
            this.uninstallButton.Text = "Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = true;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // backButton
            // 
            this.backButton.Enabled = false;
            this.backButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.backButton.Location = new System.Drawing.Point(197, 11);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(120, 30);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // topStripPanel1
            // 
            this.topStripPanel1.BackColor = System.Drawing.Color.Gray;
            this.topStripPanel1.Location = new System.Drawing.Point(0, 0);
            this.topStripPanel1.Name = "topStripPanel1";
            this.topStripPanel1.Size = new System.Drawing.Size(653, 4);
            this.topStripPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(653, 1);
            this.panel3.TabIndex = 2;
            // 
            // topStripPanel2
            // 
            this.topStripPanel2.BackColor = System.Drawing.Color.Gainsboro;
            this.topStripPanel2.Location = new System.Drawing.Point(0, 4);
            this.topStripPanel2.Name = "topStripPanel2";
            this.topStripPanel2.Size = new System.Drawing.Size(653, 4);
            this.topStripPanel2.TabIndex = 2;
            // 
            // topStripPanel3
            // 
            this.topStripPanel3.BackColor = System.Drawing.Color.DarkGray;
            this.topStripPanel3.Location = new System.Drawing.Point(0, 8);
            this.topStripPanel3.Name = "topStripPanel3";
            this.topStripPanel3.Size = new System.Drawing.Size(653, 4);
            this.topStripPanel3.TabIndex = 3;
            // 
            // logoBox
            // 
            this.logoBox.Image = global::Perun2Uninstaller.Properties.Resources.perunlogo;
            this.logoBox.Location = new System.Drawing.Point(45, 90);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(120, 120);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoBox.TabIndex = 4;
            this.logoBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(225, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Welcome to the Perun2 Uninstaller";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(225, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(319, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Press button below to confirm your decision.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(225, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "This operation cannot be reversed.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(614, 339);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.topStripPanel3);
            this.Controls.Add(this.topStripPanel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.topStripPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Uninstall Perun2";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel topStripPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel topStripPanel2;
        private System.Windows.Forms.Panel topStripPanel3;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button uninstallButton;
    }
}

