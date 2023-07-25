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
    public class Paths
    {
        private static Paths INSTANCE = new Paths();

        private bool Initialized = false;

        private const string BACKUPS_NAME = "backups";
        private const string LANGS_NAME = "langs";
        private const string SETTINGS_NAME = ".settings";
        private const string SCRIPTS_NAME = "scripts";

        private const string EXE_NAME = "perun2.exe";
        private const string GUI_NAME = "Perun2 Gui.exe";
        private const string MANAGER_NAME = "Perun2 Manager.exe";
        private const string UNINSTALL_NAME = "uninstall.exe";

        public const string ACTUALIZE_BATCH_NAME = "actualize.bat";

        public const string NEW_EXE_NAME = "newperun2.exe";
        public const string NEW_GUI_NAME = "newPerun2_Gui.exe";
        public const string NEW_MANAGER_NAME = "newPerun2_Manager.exe";
        public const string NEW_UNINSTALL_NAME = "newuninstall.exe";

        public const string INSTALLATION_NAME = "perun2_win3264_latest.exe";
        public string INSTALLATION_PATH;

        private string ROOT_PATH;
        private string BACKUPS_PATH;
        private string LANGS_PATH;
        private string SETTINGS_PATH;
        private string SCRIPTS_PATH;

        public string EXE_PATH;
        public string GUI_PATH;
        public string MANAGER_PATH;
        public string UNINSTALL_PATH;

        public string ACTUALIZE_BATCH_PATH;

        public string NEW_EXE_PATH;
        public string NEW_GUI_PATH;
        public string NEW_MANAGER_PATH;
        public string NEW_UNINSTALL_PATH;


        private Paths()
        {
            Init();
        }

        public void Init()
        {
            if (!Initialized)
            {
                Initialized = true;

                var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8);
                ROOT_PATH = Path.GetDirectoryName(path);

                BACKUPS_PATH = Path.Combine(ROOT_PATH, BACKUPS_NAME);
                LANGS_PATH = Path.Combine(ROOT_PATH, LANGS_NAME);
                SETTINGS_PATH = Path.Combine(ROOT_PATH, SETTINGS_NAME);
                SCRIPTS_PATH = Path.Combine(ROOT_PATH, SCRIPTS_NAME);

                EXE_PATH = Path.Combine(ROOT_PATH, EXE_NAME);
                GUI_PATH = Path.Combine(ROOT_PATH, GUI_NAME);
                MANAGER_PATH = Path.Combine(ROOT_PATH, MANAGER_NAME);
                UNINSTALL_PATH = Path.Combine(ROOT_PATH, UNINSTALL_NAME);

                ACTUALIZE_BATCH_PATH = Path.Combine(ROOT_PATH, ACTUALIZE_BATCH_NAME);

                NEW_EXE_PATH = Path.Combine(ROOT_PATH, NEW_EXE_NAME);
                NEW_GUI_PATH = Path.Combine(ROOT_PATH, NEW_GUI_NAME);
                NEW_MANAGER_PATH = Path.Combine(ROOT_PATH, NEW_MANAGER_NAME);
                NEW_UNINSTALL_PATH = Path.Combine(ROOT_PATH, NEW_UNINSTALL_NAME);

                INSTALLATION_PATH = Path.Combine(ROOT_PATH, INSTALLATION_NAME);

                CreateBackupsDirectory();
                CreateScriptsDirectory();
            }
        }

        public void CreateBackupsDirectory()
        {
            Directory.CreateDirectory(BACKUPS_PATH);
        }

        public void CreateScriptsDirectory()
        {
            Directory.CreateDirectory(SCRIPTS_PATH);
        }

        public static Paths GetInstance()
        {
            return INSTANCE;
        }

        public string GetExePath()
        {
            return EXE_PATH;
        }

        public string GetBackupsPath()
        {
            return BACKUPS_PATH;
        }

        public string GetLangsPath()
        {
            return BACKUPS_PATH;
        }

        public string GetSettingsPath()
        {
            return SETTINGS_PATH;
        }

        public string GetScriptsPath()
        {
            return SCRIPTS_PATH;
        }

        public string GetGuiPath()
        {
            return GUI_PATH;
        }

        public string GetManagerPath()
        {
            return MANAGER_PATH;
        }

        public string GetRootPath()
        {
            return ROOT_PATH;
        }
    }
}
