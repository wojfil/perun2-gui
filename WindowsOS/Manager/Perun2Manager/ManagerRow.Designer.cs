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

namespace Perun2Manager
{
    partial class ManagerRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameBox = new System.Windows.Forms.TextBox();
            this.editButton = new System.Windows.Forms.Button();
            this.renameButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.namePanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.namePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameBox.Font = new System.Drawing.Font("Consolas", 14F);
            this.nameBox.Location = new System.Drawing.Point(36, 5);
            this.nameBox.Name = "nameBox";
            this.nameBox.ReadOnly = true;
            this.nameBox.Size = new System.Drawing.Size(361, 22);
            this.nameBox.TabIndex = 0;
            // 
            // editButton
            // 
            this.editButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.editButton.Location = new System.Drawing.Point(420, 2);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(120, 35);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // renameButton
            // 
            this.renameButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.renameButton.Location = new System.Drawing.Point(551, 2);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(120, 35);
            this.renameButton.TabIndex = 2;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteButton.Location = new System.Drawing.Point(682, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(120, 35);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // namePanel
            // 
            this.namePanel.Controls.Add(this.pictureBox1);
            this.namePanel.Controls.Add(this.nameBox);
            this.namePanel.Location = new System.Drawing.Point(7, 4);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(402, 32);
            this.namePanel.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Perun2Manager.Properties.Resources.filesign2;
            this.pictureBox1.InitialImage = global::Perun2Manager.Properties.Resources.filesign2;
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ManagerRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.namePanel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.editButton);
            this.Name = "ManagerRow";
            this.Size = new System.Drawing.Size(804, 40);
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel namePanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
