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
using System.Windows.Forms;

namespace Perun2Gui
{
    class RecentFileMenuItem : ToolStripMenuItem
    {
        private const int MAX_NAME_LENGTH = 50;

        private string Path;
        private int Index;

        public RecentFileMenuItem(string path, int index)
            : base(GetName(path, index))
        {
            Path = path;
            Index = index;
        }

        public string GetPath()
        {
            return Path;
        }

        public int GetIndex()
        {
            return Index;
        }

        private static string GetName(string path, int index)
        {
            // this is name of item visible in the menu
            return (index + 1) + "   " + StringUtil.PathShortcut(path, MAX_NAME_LENGTH);
        }

    }
}
