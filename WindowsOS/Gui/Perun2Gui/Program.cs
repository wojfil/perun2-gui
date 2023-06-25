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
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Perun2Gui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(false));
                return;
            }

            string path = args.Last().Trim();

            if (path.Equals("*actualization*"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(true));
                return;
            }

            if (path.IsEmptyPath())
            {
                Popup.Error("Path is empty.");
            }
            else if (path.Equals("."))
            {
                string origin = Environment.CurrentDirectory;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(origin));
            }
            else
            {
                if (path.Length == 3 && path[1] == ':' && Char.IsLetter(path[0]))
                {
                    path = path.Substring(0, 2) + "\\";
                }

                if (Directory.Exists(path))
                {
                    Start_OpenDirectory(path);
                }
                else if (File.Exists(path))
                {
                    Start_OpenFile(path);
                }
                else
                {
                    string newPath = Path.Combine(Environment.CurrentDirectory, path);

                    if (File.Exists(newPath))
                    {
                        Start_OpenFile(newPath);
                    }
                    else if (Directory.Exists(newPath))
                    {
                        Start_OpenDirectory(newPath);
                    }
                    else
                    {
                        ShowPathError(path);
                    }
                }
            }
        }

        static void ShowPathError(string path)
        {
            if (path.StartsWith("::{"))
            {
                Popup.Error("You cannot do it here. Unfortunately, Perun2 works only on hard disc drives.");
            }
            else
            {
                Popup.Error("Path '" + path + "' does not lead to a valid directory nor a Perun2 script file. Note: Perun2 works only on hard disc drives.");
            }
        }

        static void Start_OpenDirectory(string path)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(path));
        }

        static void Start_OpenFile(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            if (!ext.Equals(Constants.PERUN2_EXTENSION))
            {
                Popup.Error("Only directories and files with extension '" + Constants.PERUN2_EXTENSION +
                    "' can be opened with Perun2. Current version of Perun2 does not support traversation of archive files.");

                return;
            }

            string code = "";
            string file = Path.GetFileName(path);

            try
            {
                code = File.ReadAllText(path);
            }
            catch (Exception)
            {
                Popup.Error("Something went wrong and file '" + file + "' could not be opened.");
                return;
            }

            string location = Path.GetDirectoryName(path);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(location, path, code));
        }

        public static void showActualizationResultPopup()
        {
            if (! File.Exists(Paths.GetInstance().EXE_PATH))
            {
                Popup.Error("Actualization has failed. File '" + Paths.GetInstance().EXE_PATH + "' not found.");
                return;
            }
            
            if (! File.Exists(Paths.GetInstance().UNINSTALL_PATH))
            {
                Popup.Error("Actualization has failed. File '" + Paths.GetInstance().UNINSTALL_PATH + "' not found.");
                return;
            }
            
            if (! File.Exists(Paths.GetInstance().GUI_PATH)) // this seems pointless... but let it be
            {
                Popup.Error("Actualization has failed. File '" + Paths.GetInstance().GUI_PATH + "' not found.");
                return;
            }
            
            if (! File.Exists(Paths.GetInstance().MANAGER_PATH))
            {
                Popup.Error("Actualization has failed. File '" + Paths.GetInstance().MANAGER_PATH + "' not found.");
                return;
            }

            string versionString;
            if (DataApi.GetCurrentVersion(out versionString))
            {
                Popup.Ok("Perun2 has been actualized successfully to version " + versionString + ".");
            }
            else
            {
                Popup.Ok("Perun2 has been actualized successfully.");
            }
        }
    }
}
