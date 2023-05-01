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
    class AhoNode
    {
        private char Character;
        private int Depth;
        private bool EndsWord;
        private WordType WordEndType;
        private HashSet<AhoNode> Leaves;


        public AhoNode(char character, int depth) 
        {
            Character = character;
            Depth = depth;
            EndsWord = false;
            Leaves = new HashSet<AhoNode>();
        }

        public char GetCharacter() 
        {
            return Character;
        }

        public int GetDepth()
        {
            return Depth;
        }

        public bool EndsAnyWord() 
        {
            return EndsWord;
        }

        public void SetWordEnd(WordType type) 
        {
            if (!EndsWord) 
            {
                EndsWord = true;
                WordEndType = type;
            }
        }

        public WordType GetWordEndType()
        {
            return WordEndType;
        }

        public void AddWord(string value, WordType type)
        {
            if (value.IsEmpty()) 
            {
                SetWordEnd(type);
            }
            else
            {
                char ch = value.FirstChar();
                AhoNode node = Leaves.FirstOrDefault(a => a.GetCharacter().Equals(ch));
                if (node == null)
                {
                    AhoNode newNode = new AhoNode(ch, Depth + 1);
                    newNode.AddWord(value.WithoutFirstChar(), type);
                    Leaves.Add(newNode);
                }
                else
                {
                    node.AddWord(value.WithoutFirstChar(), type);
                }
            }
        }

        public AhoNode GetNodeWith(char ch)
        {
            return Leaves.FirstOrDefault(a => a.GetCharacter().Equals(ch));
        }

    }
}
