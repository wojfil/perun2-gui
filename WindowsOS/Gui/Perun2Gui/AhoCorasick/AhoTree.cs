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

using System.Collections.Generic;
using System.Linq;

namespace Perun2Gui
{
    class AhoTree
    {
        private List<AhoNode> Roots;

        public AhoTree() 
        {
            Roots = new List<AhoNode>();
        }

        public void AddWords(IEnumerable<string> values, WordType type)
        {
            foreach (string s in values)
            {
                AddWord(s, type);
            }
        }

        public void AddWord(string value, WordType type)
        {
            char ch = value.FirstChar();
            AhoNode node = Roots.FirstOrDefault(a => a.GetCharacter().Equals(ch));
            if (node == null)
            {
                AhoNode newNode = new AhoNode(ch, 1);
                newNode.AddWord(value.WithoutFirstChar(), type);
                Roots.Add(newNode);
            }
            else
            {
                node.AddWord(value.WithoutFirstChar(), type);
            }
        }

        public AhoNode GetRootStartingWith(char ch)
        {
            return Roots.FirstOrDefault(a => a.GetCharacter().Equals(ch));
        }
    }
}
