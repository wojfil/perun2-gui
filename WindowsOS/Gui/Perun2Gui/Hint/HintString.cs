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
    class HintString
    {
        //private int Length;
        private string TrueValue;
        private string LowerValue;
        private string UpperValue;
        private string CapitalValue;

        public HintString(string value)
        {
            //Length = value.Length;
            TrueValue = value;
            LowerValue = value.ToLower();
            UpperValue = value.ToUpper();
            CapitalValue = value.StartWithCapitalLetter();
        }

        public bool Matches(string start)
        {
            return LowerValue.StartsWith(start)/* && Length != start.Length*/;
        }

        public bool HasString(string value)
        {
            return LowerValue.Equals(value);
        }

        public string GetValue(string start)
        {
            if (start.IsEmpty())
            {
                return TrueValue; // camelCase
            }
            else if (CapitalValue.StartsWith(start))
            {
                return CapitalValue; // PascalCase
            }
            else if (UpperValue.StartsWith(start))
            {
                return UpperValue; // UPPER
            }
            else if (TrueValue.StartsWith(start))
            {
                return TrueValue; // camelCase
            }

            return LowerValue;
        }

        public string GetValue()
        {
            return TrueValue;
        }
    }
}
