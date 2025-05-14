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
    partial class OptionsInfoForm
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
            this.omitPanel = new System.Windows.Forms.Panel();
            this.omitBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.omitPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // omitPanel
            // 
            this.omitPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.omitPanel.Controls.Add(this.omitBox);
            this.omitPanel.Location = new System.Drawing.Point(12, 12);
            this.omitPanel.Name = "omitPanel";
            this.omitPanel.Size = new System.Drawing.Size(549, 213);
            this.omitPanel.TabIndex = 16;
            // 
            // omitBox
            // 
            this.omitBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.omitBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.omitBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.omitBox.Location = new System.Drawing.Point(4, 4);
            this.omitBox.Multiline = true;
            this.omitBox.Name = "omitBox";
            this.omitBox.ReadOnly = true;
            this.omitBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.omitBox.Size = new System.Drawing.Size(541, 205);
            this.omitBox.TabIndex = 13;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.okButton.Location = new System.Drawing.Point(437, 240);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(124, 35);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // OptionsInfoForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BackgroundImage = global::Perun2Gui.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(573, 287);
            this.Controls.Add(this.omitPanel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OmitInfoForm_Load);
            this.omitPanel.ResumeLayout(false);
            this.omitPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel omitPanel;
        private System.Windows.Forms.TextBox omitBox;
        private System.Windows.Forms.Button okButton;

    }
}