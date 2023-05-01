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
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Gui
{
    public class SavedSettings
    {
        public const int DATA_LINES = 4;
        public const int MAX_RECENT_FILES = 20;
        public const int MAX_LINES = MAX_RECENT_FILES + DATA_LINES;

        private static SavedSettings INSTANCE = new SavedSettings();

        private bool Initialized = false;
        private GuiTheme GuiTheme;
        private string Language;
        private List<string> RecentFiles = new List<string>();
        private bool NoOmit;
        private bool Silent;

        private SavedSettings()
        {
            Paths.GetInstance().Init();
            Init();
        }

        private void Init()
        {
            if (!Initialized)
            {
                Initialized = true;
                string path = Paths.GetInstance().GetSettingsPath();

                if (File.Exists(path))
                    LoadSettings(path);
                else
                    CreateDefaultSettings(path);
            }
        }

        private void CreateDefaultSettings(string path)
        {
            GuiTheme = GuiThemeMethods.GetDefaultTheme();
            Language = "English";
            NoOmit = false;
            Silent = false;
            RecentFiles = new List<string>();
            File.Create(path).Dispose();

            Save();
        }

        private void LoadSettings(string path)
        {
            try
            {
                int counter = 0;
                string line;  
                List<string> lines = new List<string>();
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                while (counter != MAX_LINES && (line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                    counter++;
                }
                file.Close();  

                int length = lines.Count();
                if (length < DATA_LINES - 1)
                {
                    CreateDefaultSettings(path);
                    return;
                }

                string themeLine = lines[0];
                GuiTheme = GuiThemeMethods.CreateFromString(themeLine);

                string langLine = lines[2];
                Language = langLine;

                if (length == DATA_LINES - 1)
                {
                    NoOmit = false;
                    Silent = false;
                    return;
                }

                LoadFlagsFromText(lines[3]);

                RecentFiles = new List<string>();
                if (length > DATA_LINES)
                {
                    for (int i = DATA_LINES; i < length; i++)
                    {
                        RecentFiles.Add(lines[i]);
                    }
                }

                DeleteAbsentRecentFiles();

                NoOmit = false;
                Silent = false;
            }
            catch (Exception)
            {
                CreateDefaultSettings(path);
            }
        }

        public void Save()
        {
            string path = Paths.GetInstance().GetSettingsPath();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GuiTheme.ThemeToString());
            sb.AppendLine("GPL 3");
            sb.AppendLine(Language.ToString());
            sb.Append(FlagsToText());

            if (RecentFiles.Any())
            {
                foreach (string rf in RecentFiles)
                {
                    sb.AppendLine();
                    sb.Append(rf);
                }
            }

            string text = sb.ToString();
            File.WriteAllText(path, text);
        }

        private string FlagsToText()
        {
            if (NoOmit)
            {
                return Silent
                    ? "NoOmitSilent"
                    : "NoOmit";
            }
            else
            {
                return Silent
                    ? "Silent"
                    : "";
            }
        }

        private void LoadFlagsFromText(string text)
        {
            if (text.Equals("NoOmitSilent"))
            {
                NoOmit = true;
                Silent = true;
            }
            else if (text.Equals("NoOmit"))
            {
                NoOmit = true;
                Silent = false;
            }
            else if (text.Equals("Silent"))
            {
                NoOmit = false;
                Silent = true;
            }
            else
            {
                NoOmit = false;
                Silent = false;
            }
        }

        public static SavedSettings GetInstance()
        {
            return INSTANCE;
        }

        public GuiTheme GetGuiTheme()
        {
            return GuiTheme;
        }

        public string GetLanguage()
        {
            return Language;
        }

        public List<string> GetRecentFiles()
        {
            return RecentFiles;
        }

        public void SetGuiTheme(GuiTheme guiTheme)
        {
            GuiTheme = guiTheme;
            Save();
        }

        public void RecentFileMoveFront(int index)
        {
            if (index == 0)
                return;

            for (int i = 0; i < index; i++)
            {
                int x = index - i;
                string tmp = RecentFiles[x];
                RecentFiles[x] = RecentFiles[x - 1];
                RecentFiles[x - 1] = tmp;
            }

            Save();
        }

        public void DeleteRecentFile(int index)
        {
            RecentFiles.RemoveAt(index);
            Save();
        }

        public void AddRecentFile(string file)
        {
            int count = RecentFiles.Count();

            for (int i = 0; i < count; i++)
            {
                if (RecentFiles[i].Equals(file))
                {
                    if (i != 0)
                    {
                        RecentFileMoveFront(i);
                    }

                    return;
                }
            }

            RecentFiles.Insert(0, file);

            if (count == MAX_RECENT_FILES)
            {
                RecentFiles.RemoveAt(MAX_RECENT_FILES);
            }

            Save();
        }

        public bool GetNoOmit()
        {
            return NoOmit;
        }

        public void ReverseNoOmit()
        {
            NoOmit = !NoOmit;
            Save();
        }

        public bool GetSilent()
        {
            return Silent;
        }

        public void ReverseSilent()
        {
            Silent = !Silent;
            Save();
        }

        public void DeleteAbsentRecentFiles()
        {
            int prevCount = RecentFiles.Count();
            RecentFiles.RemoveAll(rf => !File.Exists(rf));

            if (prevCount != RecentFiles.Count())
                Save();
        }
    }
}
