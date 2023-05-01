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

namespace Perun2Manager
{

    public static class StringUtil
    {
        static readonly string POINTS = "...";
        const int POINTS_COUNT = 3;


        public static string PathAbbreviation(string path, int maxLength)
        {
            int length = path.Length;

            if (length <= maxLength)
                return path;

            string right = path.Substring(length - (maxLength - 2 * POINTS_COUNT));
            int index = right.IndexOf('\\');

            return index == -1
                ? path.Substring(0, POINTS_COUNT) + POINTS + right
                : path.Substring(0, POINTS_COUNT) + POINTS + right.Substring(index);
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
