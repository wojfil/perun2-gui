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
using System.Text.RegularExpressions;
using Perun2Gui.Properties;

namespace Perun2Gui
{
    public partial class ReplaceForm : Form
    {
        MainForm MainForm;

        public ReplaceForm(MainForm mainForm, string replaceInput, string replaceOutput)
        {
            MainForm = mainForm;
            InitializeComponent();

            this.Icon = Resources.perun256;
            inputBox.Text = replaceInput;
            outputBox.Text = replaceOutput;
            RefreshEnableness();
            DarkMode();

            inputBox.Select();
            if (!inputBox.Text.Equals(String.Empty))
            {
                inputBox.SelectionStart = inputBox.Text.Length;
            }
        }

        public string GetInputText()
        {
            return inputBox.Text;
        }

        public string GetOutputText()
        {
            return outputBox.Text;
        }

        private void inputBox_TextChanged_1(object sender, EventArgs e)
        {
            RefreshEnableness();
        }

        private void outputBox_TextChanged(object sender, EventArgs e)
        {
            RefreshEnableness();
        }

        private void RefreshEnableness()
        {
            replaceAllButton.Enabled = 
                   !inputBox.Text.Equals(String.Empty)
                && !inputBox.Text.Equals(outputBox.Text);
        }

        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            string i = GetInputText();
            string o = GetOutputText();

            if (i.Equals(String.Empty))
            {
                Popup.Error("Empty phrase cannot be replaced.");
                return;
            }

            string code = MainForm.GetCode();
            int occurences = code.CountSubstring(i);

            if (occurences == 0)
            {
                Popup.Ok("There was nothing to replace.");
                return;
            }
            else
            {
                string newCode = code.Replace(i, o);
                MainForm.SetCode(newCode);

                if (occurences == 1)
                    Popup.Ok("1 phrase has been replaced.");
                else
                    Popup.Ok(occurences + " phrases have been replaced.");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);
            this.inputBox.BackColor = Color.FromArgb(55, 55, 55);
            this.inputPanel.BackColor = Color.FromArgb(55, 55, 55);
            this.outputBox.BackColor = Color.FromArgb(55, 55, 55);
            this.outputPanel.BackColor = Color.FromArgb(55, 55, 55);

            this.label1.BackColor = Color.FromArgb(30, 30, 30);
            this.label2.BackColor = Color.FromArgb(30, 30, 30);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
