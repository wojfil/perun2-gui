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
    public static class StringUtil
    {
        static readonly string POINTS = "...";
        const int POINTS_COUNT = 3;


        public static string PathShortcut(string path, int maxLength)
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

        public static string NameShortcut(string name, int maxLength)
        {
            int length = name.Length;

            if (length <= maxLength - POINTS_COUNT + 1)
                return name;
            else
                return POINTS + name.Substring(length - maxLength + POINTS_COUNT);
        }

        public static bool IsEmpty(this string str)
        {
            return str == null || str.Length == 0;
        }

        public static char FirstChar(this string str)
        {
            return str[0];
        }

        public static string WithoutFirstChar(this string str)
        {
            return str.Substring(1);
        }

        public static string WithoutLastChar(this string str)
        {
            return str.Substring(0, str.Length - 1);
        }

        public static bool ContainsOnlyDigits(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        public static int DigitAtIndex(this string str, int index)
        {
            int a = str[index] - '0';
            return a;
        }

        public static bool IsHex(this char ch)
        {
            return (ch >= '0' && ch <= '9')
                || (ch >= 'a' && ch <= 'f');
        }

        public static int CountLines(this string str)
        {
            return str.Split('\n').Length;
        }

        public static bool IsEmptyPath(this string str)
        {
            if (str == null || str.Length == 0)
            {
                return true;
            }

            foreach (char ch in str)
            {
                switch (ch)
                {
                    case ' ':
                    case '\\':
                    case '/':
                    case '|':
                        {
                        break;
                    }
                    default:
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsNewLine(this char ch)
        {
            return ch == '\n';
        }

        public static bool IsSpace(this char ch)
        {
            return Char.IsWhiteSpace(ch);
        }

        public static bool IsPerun2WordChar(this char ch)
        {
            return Char.IsLetterOrDigit(ch) || ch == '_';
        }

        public static string StartWithCapitalLetter(this string str)
        {
            return Char.ToUpper(str.First()).ToString() + str.Substring(1);
        }

        public static int CountSubstring(this string text, string value)
        {
            int count = 0, minIndex = text.IndexOf(value, 0);
            while (minIndex != -1)
            {
                minIndex = text.IndexOf(value, minIndex + value.Length);
                count++;
            }
            return count;
        }

        public static bool IsValidVersion(this string str)
        {
            if (str.IsEmpty())
            {
                return false;
            }

            return Char.IsDigit(str.First());
        }

        public static string Last(this string str, int amount)
        {
            if (str.IsEmpty())
            {
                return "";
            }

            int length = str.Length;
            if (length <= amount)
            {
                return str;
            }
            else {
                return str.Substring(length - amount);
            }
        }




    }
}
