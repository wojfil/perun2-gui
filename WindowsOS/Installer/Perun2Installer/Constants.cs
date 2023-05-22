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
using System.Drawing;

namespace Perun2Installer
{
    public static class Constants
    {
        public const int TOP_STRIPS = 3;
        public const int TOP_STRIP_HEIGHT = 4;
        public static Color TOP_STRIP_COLOR = Color.FromArgb(150, 150, 150);
        public static Color TOP_STRIP_COLOR_2 = Color.FromArgb(200, 200, 200);
        public static Color TOP_STRIP_COLOR_3 = Color.FromArgb(230, 230, 230);

        public const int FORM_WIDTH = 630;
        public const int PANEL_START_X = 180;


        public const int PATH_SHORTCUT_LENGTH = 40;

        public static readonly string RECOMMENDED_SYSTEM = "Windows OS 7";

        public static readonly string FOLDER_MAIN = "Perun2";
        public static readonly string FOLDER_BACKUPS = "backups";
        public static readonly string FOLDER_SCRIPTS = "scripts";

        public static readonly string FILE_GUI = "Perun2 Gui.exe";
        public static readonly string FILE_MANANGER = "Perun2 Manager.exe";
        public static readonly string FILE_PERUN2 = "perun2.exe";
        public static readonly string FILE_UNINSTALL = "uninstall.exe";
        public static readonly string FILE_SETTINGS = ".settings";
        public static readonly string FILE_ICON = "perun2ico.ico";
        public static readonly string FILE_ACTUALIZE = "actualize.bat";

        public static readonly string PERU_EXTENSION = ".peru";

        public static readonly string PERUN2_HERE = "Perun2 Here";
        public static readonly string PERUN2 = "Perun2";
        public static readonly string PERUN2_RUN_NOW = "Run Perun2 now";

        public static readonly string PUBLISHER = "WojFil Games";

        public static readonly string SCRIPT_DELETE_EMPTY_DIRECTORIES = "Delete empty directories.peru";
        public static readonly string SCRIPT_SELECT_ALL = "Select all.peru";

        public static readonly string WEBSITE =      "http://perun2.org";
        public static readonly string WEBSITE_DOCS = "http://perun2.org//docs";
        public static readonly string REGISTRY_UNINSTALL_GUID = "{BD9E139C-380D-4775-88E1-28EE77B626AF}";
        public static readonly string SHORTCUT_NAME = "Perun2.lnk";

    }
}
