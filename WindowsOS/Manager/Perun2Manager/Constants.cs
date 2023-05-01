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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perun2Manager
{
    static class Constants
    {
        public const int PATH_ABBREVIATION_LENGTH = 30;
        public const string PERUN2_EXTENSION = ".peru";
        public const string DIRNAME_SCRIPTS = "scripts";

        public const string FILENAME_GUI = "Perun2 Gui.exe";
        public const string FILENAME_MANAGER = "Perun2 Manager.exe";
        public const string FILENAME_EXE = "perun2.exe";

        // an average key name looks like this: "u0001Select all"
        // starts with KEYNAME_PREFIX and after that there are some digits and finally is extensionless file name
        public const string KEYNAME_PREFIX = "u";
        public const int KEYNAME_NUMERATION_DIGITS = 4;
        public static readonly int KEYNAME_TOTAL_PREFIX_LENGTH = KEYNAME_NUMERATION_DIGITS + KEYNAME_PREFIX.Length;
        public static readonly int KEYNAME_MINIMUM_LENGTH = KEYNAME_NUMERATION_DIGITS + KEYNAME_PREFIX.Length + 1;
        public const string KEYNAME_ADD_NEW = "v_Add";

        public const string REGISTRY_GLOBAL_SCRIPTS_ROOT = "*\\Perun2\\GlobalScripts\\shell";
        public const string REGISTRY_COMMAND = "\\command";

        public const int NAME_LENGTH_LIMIT = 160;
        public const int CONTEXT_MENU_LIMIT = 16;

        public static readonly Color COLOR_FORMBACK_DAY = Color.FromArgb(235, 235, 235);
        public static readonly Color COLOR_TEXTBACK_DAY = Color.FromArgb(251, 251, 251);
        public static readonly Color COLOR_TEXT_SELECTION = Color.FromArgb(0, 128, 128, 128);

    }
}
