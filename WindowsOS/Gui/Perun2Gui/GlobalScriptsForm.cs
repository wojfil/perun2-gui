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
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Security.Permissions;
using Perun2Gui.Properties;

namespace Perun2Gui
{
    public partial class GlobalScriptsForm : Form
    {
        private MainForm MainForm;
        public string ScriptName;
        public string ScriptPath;

        public GlobalScriptsForm(MainForm mainForm)
        {
            MainForm = mainForm;
            InitializeComponent();
            this.Icon = Resources.perun256;
            DarkMode();
        }

        private void DarkMode()
        {
            _ = new DarkModeCS(this);
            this.nameBox.BorderStyle = BorderStyle.None;
            this.namePanel.BackColor = this.nameBox.BackColor;

            this.topLabel.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ScriptName = nameBox.Text.Trim();

            if (!IsValidPerun2ScriptName(ScriptName))
            {
                return;
            }

            ScriptPath = GetPathToScript(ScriptName);

            try
            {
                File.Create(ScriptPath).Dispose();
                RefreshRegistryItems();
            }
            catch (Exception)
            {
                Popup.Error("Something wrong happened and a new script file could not be created.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private string GetPathToScript(string name)
        {
            return Path.Combine(Paths.GetInstance().GetScriptsPath(), name) + Constants.PERUN2_EXTENSION;
        }

        private void EnsureScriptsDirectoryExists()
        {
            var path = Paths.GetInstance().GetScriptsPath();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        private void RefreshRegistryItems()
        {
            EnsureScriptsDirectoryExists();

            var keys = GetRegistryKeys();
            var keyNames = keys.TrimPrefix(Constants.KEYNAME_TOTAL_PREFIX_LENGTH);
            var files = Filesystem.GetGlobalScriptFiles();

            if (keyNames.EqualsToList(files))
            {
                return;
                // if registry keys are the same as script files
                // there is nothing to do
                // just escape from here
            }

            ClearAndSetNewKeys(keys, files);
        }
        private List<string> GetRegistryKeys()
        {
            List<string> values = new List<string>();

            using (RegistryKey MyReg = Registry.ClassesRoot.OpenSubKey(Constants.REGISTRY_GLOBAL_SCRIPTS_ROOT))
            {
                var subKeys = MyReg.GetSubKeyNames();

                foreach (string s in subKeys)
                {
                    if (IsGlobalScriptKey(s))
                    {
                        values.Add(s);
                    }
                }
            }

            values.Sort();
            return values;
        }

        private void ClearAndSetNewKeys(List<string> oldKeys, List<string> newNames)
        {
            foreach (var o in oldKeys)
            {
                DeleteRegistryItem(o);
            }

            int i = 0;
            foreach (var n in newNames)
            {
                AddRegistryItem(n, i);
                i++;
            }
        }

        private void AddRegistryItem(string name, int id)
        {
            string regName = GetKeyName(name, id);
            string MenuName = Constants.REGISTRY_GLOBAL_SCRIPTS_ROOT + "\\" + regName;
            string Command = MenuName + Constants.REGISTRY_COMMAND;

            try
            {
                using (RegistryKey reg = Registry.ClassesRoot.CreateSubKey(MenuName))
                {
                    reg.SetValue("", name);
                    reg.SetValue("MUIVerb", name);
                }

                string exe = Paths.GetInstance().GetExePath();
                string script = GetPathToScript(name);

                using (RegistryKey reg2 = Registry.ClassesRoot.CreateSubKey(Command))
                {
                    string c = "\"" + exe + "\" -s -d \"%V \" \"" + script + "\"";
                    reg2.SetValue("", c);
                }
            }
            catch (Exception ex)
            {
                Popup.Error(ex.ToString());
            }
        }
        public static void DeleteRegistryItem(string name)
        {
            string MenuName = Constants.REGISTRY_GLOBAL_SCRIPTS_ROOT + "\\" + name;
            string Command = MenuName + Constants.REGISTRY_COMMAND;

            try
            {
                using (RegistryKey reg = Registry.ClassesRoot.OpenSubKey(Command))
                {
                    Registry.ClassesRoot.DeleteSubKey(Command);
                }

                using (RegistryKey reg2 = Registry.ClassesRoot.OpenSubKey(MenuName))
                {
                    Registry.ClassesRoot.DeleteSubKey(MenuName);
                }
            }
            catch (Exception ex)
            {
                Popup.Error(ex.ToString());
            }
        }


        private string GetKeyName(string name, int id)
        {
            return Constants.KEYNAME_PREFIX + id.ToString().PadLeft(Constants.KEYNAME_NUMERATION_DIGITS, '0') + name;
        }

        private void OpenScriptInGUI(string name, string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Paths.GetInstance().GetGuiPath();
            startInfo.Arguments = "\"" + path + "\"";
            Process.Start(startInfo);
        }

        private bool IsGlobalScriptKey(string name)
        {
            if (name.Length < Constants.KEYNAME_MINIMUM_LENGTH)
            {
                return false;
            }

            if (!name.StartsWith(Constants.KEYNAME_PREFIX))
            {
                return false;
            }

            for (int i = 0; i < Constants.KEYNAME_NUMERATION_DIGITS; i++)
            {
                if (!Char.IsDigit(name[Constants.KEYNAME_PREFIX.Length + i]))
                {
                    return false;
                }
            }

            return true;
        }

        private int CountRegistryKeys()
        {
            int result = 0;

            using (RegistryKey MyReg = Registry.ClassesRoot.OpenSubKey(Constants.REGISTRY_GLOBAL_SCRIPTS_ROOT))
            {
                var subKeys = MyReg.GetSubKeyNames();

                foreach (string s in subKeys)
                {
                    if (IsGlobalScriptKey(s))
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }



        private bool IsValidPerun2ScriptName(string name)
        {
            if (name.Length == 0)
            {
                Popup.Error("The name is empty.");
                return false;
            }
            if (name.Length > Constants.GLOBAL_SCRIPT_NAME_LENGTH_LIMIT)
            {
                Popup.Error("The name is too long.");
                return false;
            }

            if (!IsFileNameValid(name))
            {
                return false;
            }

            string path = GetPathToScript(name);

            if (File.Exists(path))
            {
                Popup.Error("A script with this name already exists. Choose something else.");
                return false;
            }

            return true;
        }
        
        private static void NotAllowedCharException(char ch)
        {
            Popup.Error("Character " + ch + " is not allowed in name.");
        }

        public static bool IsFileNameValid(string name)
        {
            int id = name.IndexOfAny(Path.GetInvalidFileNameChars());

            if (id >= 0)
            {
                NotAllowedCharException(name[id]);
                return false;
            }

            foreach (char ch in name)
            {
                switch (ch)
                {
                    case ':':
                    case '\\':
                    case '/':
                    case '|':
                    {
                        NotAllowedCharException(ch);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
