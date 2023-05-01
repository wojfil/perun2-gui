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
    public static class Extensions
    {
        public static bool EqualsToList<T>(this List<T> list, List<T> other)
        {
            if (list.Count() != other.Count())
            {
                return false;
            }

            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Equals(other[i]))
                {
                    return false;
                }
            }

            return true;
        }


        public static List<string> TrimPrefix(this List<string> list, int length)
        {
            return list.Select(x => x.Substring(length)).ToList();
        }


        public static bool IsValidPerun2ScriptName(this string s)
        {
            if (s.Length == 0)
            {
                Popup.Error("The name is empty.");
                return false;
            }
            if (s.Length > Constants.NAME_LENGTH_LIMIT)
            {
                Popup.Error("The name is too long.");
                return false;
            }

            if (!StringUtil.IsFileNameValid(s))
            {
                return false;
            }

            string path = Filesystem.GetPathToScript(s);

            if (File.Exists(path))
            {
                Popup.Error("A script with this name already exists. Choose something else.");
                return false;
            }

            return true;
        }



    }
}
