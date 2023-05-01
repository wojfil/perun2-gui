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
using System.Text;

namespace FastColoredTextBoxNS
{
    public class LinesAccessor : IList<string>
    {
        IList<Line> ts;

        public LinesAccessor(IList<Line> ts)
        {
            this.ts = ts;
        }

        public int IndexOf(string item)
        {
            for (int i = 0; i < ts.Count; i++)
                if (ts[i].Text == item)
                    return i;

            return -1;
        }

        public void Insert(int index, string item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public string this[int index]
        {
            get
            {
                return ts[index].Text;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(string item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string item)
        {
            for (int i = 0; i < ts.Count; i++)
                if (ts[i].Text == item)
                    return true;

            return false;
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            for (int i = 0; i < ts.Count; i++)
                array[i + arrayIndex] = ts[i].Text;
        }

        public int Count
        {
            get { return ts.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(string item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < ts.Count; i++)
                yield return ts[i].Text;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
