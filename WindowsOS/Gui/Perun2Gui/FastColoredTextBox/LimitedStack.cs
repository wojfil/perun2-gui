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
    /// Limited stack
    /// </summary>
    public class LimitedStack<T>
    {
        T[] items;
        int count;
        int start;

        /// <summary>
        /// Max stack length
        /// </summary>
        public int MaxItemCount
        {
            get { return items.Length; }
        }

        /// <summary>
        /// Current length of stack
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxItemCount">Maximum length of stack</param>
        public LimitedStack(int maxItemCount)
        {
            items = new T[maxItemCount];
            count = 0;
            start = 0;
        }

        /// <summary>
        /// Pop item
        /// </summary>
        public T Pop()
        {
            if (count == 0)
                throw new Exception("Stack is empty");

            int i = LastIndex;
            T item = items[i];
            items[i] = default(T);

            count--;

            return item;
        }

        int LastIndex
        {
            get { return (start + count - 1) % items.Length; }
        }

        /// <summary>
        /// Peek item
        /// </summary>
        public T Peek()
        {
            if (count == 0)
                return default(T);

            return items[LastIndex];
        }

        /// <summary>
        /// Push item
        /// </summary>
        public void Push(T item)
        {
            if (count == items.Length)
                start = (start + 1) % items.Length;
            else
                count++;

            items[LastIndex] = item;
        }

        /// <summary>
        /// Clear stack
        /// </summary>
        public void Clear()
        {
            items = new T[items.Length];
            count = 0;
            start = 0;
        }

        public T[] ToArray()
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
                result[i] = items[(start + i) % items.Length];
            return result;
        }
    }
}