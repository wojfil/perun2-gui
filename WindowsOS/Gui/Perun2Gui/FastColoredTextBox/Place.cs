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

namespace FastColoredTextBoxNS
{
    /// <summary>
    /// Line index and char index
    /// </summary>
    public struct Place : IEquatable<Place>
    {
        public int iChar;
        public int iLine;

        public Place(int iChar, int iLine)
        {
            this.iChar = iChar;
            this.iLine = iLine;
        }

        public void Offset(int dx, int dy)
        {
            iChar += dx;
            iLine += dy;
        }

        public bool Equals(Place other)
        {
            return iChar == other.iChar && iLine == other.iLine;
        }

        public override bool Equals(object obj)
        {
            return (obj is Place) && Equals((Place)obj);
        }

        public override int GetHashCode()
        {
            return iChar.GetHashCode() ^ iLine.GetHashCode();
        }

        public static bool operator !=(Place p1, Place p2)
        {
            return !p1.Equals(p2);
        }

        public static bool operator ==(Place p1, Place p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator <(Place p1, Place p2)
        {
            if (p1.iLine < p2.iLine) return true;
            if (p1.iLine > p2.iLine) return false;
            if (p1.iChar < p2.iChar) return true;
            return false;
        }

        public static bool operator <=(Place p1, Place p2)
        {
            if (p1.Equals(p2)) return true;
            if (p1.iLine < p2.iLine) return true;
            if (p1.iLine > p2.iLine) return false;
            if (p1.iChar < p2.iChar) return true;
            return false;
        }

        public static bool operator >(Place p1, Place p2)
        {
            if (p1.iLine > p2.iLine) return true;
            if (p1.iLine < p2.iLine) return false;
            if (p1.iChar > p2.iChar) return true;
            return false;
        }

        public static bool operator >=(Place p1, Place p2)
        {
            if (p1.Equals(p2)) return true;
            if (p1.iLine > p2.iLine) return true;
            if (p1.iLine < p2.iLine) return false;
            if (p1.iChar > p2.iChar) return true;
            return false;
        }

        public static Place operator +(Place p1, Place p2)
        {
            return new Place(p1.iChar + p2.iChar, p1.iLine + p2.iLine);
        }

        public static Place Empty
        {
            get { return new Place(); }
        }

        public override string ToString()
        {
            return "(" + iChar + "," + iLine + ")";
        }
    }
}
