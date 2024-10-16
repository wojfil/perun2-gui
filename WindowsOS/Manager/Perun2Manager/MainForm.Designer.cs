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
            this.components = new System.ComponentModel.Container();
            this.addNewButton = new System.Windows.Forms.Button();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.outerPanel = new System.Windows.Forms.Panel();
            this.pathPanel = new System.Windows.Forms.Panel();
            this.fileImageBox = new System.Windows.Forms.PictureBox();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.pathMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enterPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.seeAllButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.outerPanel.SuspendLayout();
            this.pathPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileImageBox)).BeginInit();
            this.pathMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // addNewButton
            // 
            this.addNewButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addNewButton.Location = new System.Drawing.Point(26, 12);
            this.addNewButton.Name = "addNewButton";
            this.addNewButton.Size = new System.Drawing.Size(120, 35);
            this.addNewButton.TabIndex = 2;
            this.addNewButton.Text = "Add new";
            this.addNewButton.UseVisualStyleBackColor = true;
            this.addNewButton.Click += new System.EventHandler(this.addNewButton_Click);
            // 
            // tablePanel
            // 
            this.tablePanel.AutoSize = true;
            this.tablePanel.ColumnCount = 1;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.Location = new System.Drawing.Point(3, 3);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 2;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.Size = new System.Drawing.Size(813, 63);
            this.tablePanel.TabIndex = 3;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.mainPanel.Controls.Add(this.tablePanel);
            this.mainPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mainPanel.Location = new System.Drawing.Point(2, 2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(833, 327);
            this.mainPanel.TabIndex = 4;
            // 
            // outerPanel
            // 
            this.outerPanel.AutoScroll = true;
            this.outerPanel.BackColor = System.Drawing.SystemColors.Control;
            this.outerPanel.Controls.Add(this.mainPanel);
            this.outerPanel.Location = new System.Drawing.Point(12, 56);
            this.outerPanel.Name = "outerPanel";
            this.outerPanel.Size = new System.Drawing.Size(837, 331);
            this.outerPanel.TabIndex = 6;
            // 
            // pathPanel
            // 
            this.pathPanel.Controls.Add(this.fileImageBox);
            this.pathPanel.Controls.Add(this.pathBox);
            this.pathPanel.Location = new System.Drawing.Point(157, 13);
            this.pathPanel.Name = "pathPanel";
            this.pathPanel.Size = new System.Drawing.Size(398, 34);
            this.pathPanel.TabIndex = 7;
            // 
            // fileImageBox
            // 
            this.fileImageBox.BackColor = System.Drawing.Color.Transparent;
            this.fileImageBox.Image = global::Perun2Manager.Properties.Resources.locsign2;
            this.fileImageBox.InitialImage = global::Perun2Manager.Properties.Resources.locsign2;
            this.fileImageBox.Location = new System.Drawing.Point(6, 6);
            this.fileImageBox.Name = "fileImageBox";
            this.fileImageBox.Size = new System.Drawing.Size(20, 20);
            this.fileImageBox.TabIndex = 1;
            this.fileImageBox.TabStop = false;
            // 
            // pathBox
            // 
            this.pathBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pathBox.ContextMenuStrip = this.pathMenuStrip;
            this.pathBox.Font = new System.Drawing.Font("Consolas", 14F);
            this.pathBox.Location = new System.Drawing.Point(36, 5);
            this.pathBox.Name = "pathBox";
            this.pathBox.ReadOnly = true;
            this.pathBox.Size = new System.Drawing.Size(361, 22);
            this.pathBox.TabIndex = 0;
            // 
            // pathMenuStrip
            // 
            this.pathMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterPathToolStripMenuItem});
            this.pathMenuStrip.Name = "pathMenuStrip";
            this.pathMenuStrip.Size = new System.Drawing.Size(102, 26);
            // 
            // enterPathToolStripMenuItem
            // 
            this.enterPathToolStripMenuItem.Name = "enterPathToolStripMenuItem";
            this.enterPathToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.enterPathToolStripMenuItem.Text = "Enter";
            this.enterPathToolStripMenuItem.Click += new System.EventHandler(this.enterPathToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(735, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 9;
            // 
            // seeAllButton
            // 
            this.seeAllButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.seeAllButton.Location = new System.Drawing.Point(702, 12);
            this.seeAllButton.Name = "seeAllButton";
            this.seeAllButton.Size = new System.Drawing.Size(120, 35);
            this.seeAllButton.TabIndex = 10;
            this.seeAllButton.Text = "See all";
            this.seeAllButton.UseVisualStyleBackColor = true;
            this.seeAllButton.Click += new System.EventHandler(this.seeAllButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Perun2Manager.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(862, 396);
            this.Controls.Add(this.seeAllButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathPanel);
            this.Controls.Add(this.outerPanel);
            this.Controls.Add(this.addNewButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Perun2 Manager";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.outerPanel.ResumeLayout(false);
            this.pathPanel.ResumeLayout(false);
            this.pathPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileImageBox)).EndInit();
            this.pathMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addNewButton;
        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel outerPanel;
        private System.Windows.Forms.Panel pathPanel;
        private System.Windows.Forms.PictureBox fileImageBox;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.ContextMenuStrip pathMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem enterPathToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button seeAllButton;
    }
}

