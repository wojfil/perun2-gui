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
    partial class VersionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionForm));
            this.newPanel = new System.Windows.Forms.Panel();
            this.newBox = new System.Windows.Forms.TextBox();
            this.oldPanel = new System.Windows.Forms.Panel();
            this.oldBox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.actualizeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.newPanel.SuspendLayout();
            this.oldPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // newPanel
            // 
            this.newPanel.Controls.Add(this.newBox);
            this.newPanel.Location = new System.Drawing.Point(136, 61);
            this.newPanel.Name = "newPanel";
            this.newPanel.Size = new System.Drawing.Size(134, 28);
            this.newPanel.TabIndex = 13;
            // 
            // newBox
            // 
            this.newBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.newBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.newBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.newBox.ForeColor = System.Drawing.Color.Black;
            this.newBox.Location = new System.Drawing.Point(3, 3);
            this.newBox.Name = "newBox";
            this.newBox.ReadOnly = true;
            this.newBox.Size = new System.Drawing.Size(128, 23);
            this.newBox.TabIndex = 15;
            this.newBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // oldPanel
            // 
            this.oldPanel.Controls.Add(this.oldBox);
            this.oldPanel.Location = new System.Drawing.Point(136, 24);
            this.oldPanel.Name = "oldPanel";
            this.oldPanel.Size = new System.Drawing.Size(134, 28);
            this.oldPanel.TabIndex = 12;
            // 
            // oldBox
            // 
            this.oldBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.oldBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.oldBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.oldBox.ForeColor = System.Drawing.Color.Black;
            this.oldBox.Location = new System.Drawing.Point(3, 3);
            this.oldBox.Name = "oldBox";
            this.oldBox.ReadOnly = true;
            this.oldBox.Size = new System.Drawing.Size(128, 23);
            this.oldBox.TabIndex = 14;
            this.oldBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cancelButton.Location = new System.Drawing.Point(151, 127);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(124, 35);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // actualizeButton
            // 
            this.actualizeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actualizeButton.Location = new System.Drawing.Point(21, 127);
            this.actualizeButton.Name = "actualizeButton";
            this.actualizeButton.Size = new System.Drawing.Size(124, 35);
            this.actualizeButton.TabIndex = 10;
            this.actualizeButton.Text = "Actualize";
            this.actualizeButton.UseVisualStyleBackColor = true;
            this.actualizeButton.Click += new System.EventHandler(this.actualizeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(24, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Latest version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(33, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "Your version:";
            // 
            // VersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BackgroundImage = global::Perun2Gui.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(294, 177);
            this.Controls.Add(this.newPanel);
            this.Controls.Add(this.oldPanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.actualizeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Perun2 Version";
            this.newPanel.ResumeLayout(false);
            this.newPanel.PerformLayout();
            this.oldPanel.ResumeLayout(false);
            this.oldPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel newPanel;
        private System.Windows.Forms.Panel oldPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button actualizeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox oldBox;
        private System.Windows.Forms.TextBox newBox;

    }
}