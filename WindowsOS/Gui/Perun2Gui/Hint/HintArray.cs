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

namespace Perun2Gui.Hint
{
    class HintArray
    {
        private HintString[] Strings;
        private HintType Type;
        private HintString RecentValue;

        public HintArray(string[] strings, HintType type) : this(strings)
        {
            Type = type;
        }

        public HintArray(string[] strings)
        {
            Type = HintType.Normal;
            int length = strings.Length;
            Strings = new HintString[length];
            for (int i = 0; i < length; i++)
            {
                Strings[i] = new HintString(strings[i]);
            }
        }

        public HintType GetHintType()
        {
            return Type;
        }

        public string GetRecentValue(string start)
        {
            return RecentValue.GetValue(start);
        }

        public bool Matches(string start)
        {
            if (start.IsEmpty())
            {
                RecentValue = Strings.First();
                return true;
            }

            foreach (var s in Strings)
            {
                if (s.Matches(start))
                {
                    RecentValue = s;
                    return true;
                }
            }

            return false;
        }

        public bool ContainsValue(string prev)
        {
            foreach (var s in Strings)
            {
                if (s.HasString(prev))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
