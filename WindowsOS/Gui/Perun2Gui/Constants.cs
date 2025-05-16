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

namespace Perun2Gui
{
    static class Constants
    {
        public static string[] MONTH_NAMES = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", 
            "September", "October", "November", "December" };
        public static string[] WEEK_DAY_NAMES = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday",  "Saturday", "Sunday" };

        public const int GLOBAL_SCRIPT_NAME_LENGTH_LIMIT = 160;
        public const int GLOBAL_SCRIPT_ITEMS_LIMIT = 16;

        // if the action is to open 10 or more files, ask user for confirmation
        public const int A_LOT_TO_OPEN_THREHOLD = 10;

        public const bool ACTUALIZATIONS_ENABLED = true;

        public static readonly string WEBSITE_ROOT = @"https://perun2.org";
        public static readonly string VERSION_API = WEBSITE_ROOT + @"/api/win3264/version";
        public static readonly string DOCS_ADDRESS = WEBSITE_ROOT + @"/docs";

        public static readonly string ACTUALIZATION_FILE_PERUN2 =     WEBSITE_ROOT + @"/api/win3264/newperun2.exe";
        public static readonly string ACTUALIZATION_FILE_UNINSTALL =  WEBSITE_ROOT + @"/api/win3264/newuninstall.exe";
        public static readonly string ACTUALIZATION_FILE_GUI =        WEBSITE_ROOT + @"/api/win3264/newPerun2_Gui.exe";
        public static readonly string ACTUALIZATION_FILE_MANAGER =    WEBSITE_ROOT + @"/api/win3264/newPerun2_Manager.exe";
        public static readonly string INSTALLATION_FILE_PERUN2 =      WEBSITE_ROOT + @"/api/win3264/perun2_win3264_latest.exe";

        public const string LANGUAGE_NAME = "Perun2";
        public const string PERUN2_EXTENSION = ".peru";
        public const string REGISTRY_GLOBAL_SCRIPTS_ROOT = "*\\Perun2\\GlobalScripts\\shell";
        public const string REGISTRY_COMMAND = "\\command";
        public const string KEYNAME_PREFIX = "u";
        public const int KEYNAME_NUMERATION_DIGITS = 4;
        public static readonly int KEYNAME_TOTAL_PREFIX_LENGTH = KEYNAME_NUMERATION_DIGITS + KEYNAME_PREFIX.Length;
        public static readonly int KEYNAME_MINIMUM_LENGTH = KEYNAME_NUMERATION_DIGITS + KEYNAME_PREFIX.Length + 1;
        public const string KEYNAME_ADD_NEW = "v_Add";

    }
}
