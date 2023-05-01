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

namespace Perun2Gui
{
    public enum GuiTheme
    {
        Day,
        Night
    }

    public static class GuiThemeMethods
    {
        public static GuiTheme GetDefaultTheme()
        {
            return GuiTheme.Day;
        }

        public static string ThemeToString(this GuiTheme guiTheme)
        {
            switch (guiTheme)
            {
                case GuiTheme.Day:
                    return "Day";
                default:
                    return "Night";
            }
        }

        public static GuiTheme CreateFromString(string name)
        {
            switch (name)
            {
                case "Day":
                    return GuiTheme.Day;
                case "Night":
                    return GuiTheme.Night;
                default:
                    return GuiThemeMethods.GetDefaultTheme();
            }
        }
    }
}
