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


namespace Perun2Gui
{
    class AhoComparer
    {
        private AhoTree Tree;
        private HashSet<AhoNode> Nodes_1;
        private HashSet<AhoNode> Nodes_2;
        private bool IsFirst;
        private int RecentWordLength;
        private WordType RecentWordType;
        private bool WaitingForStart;
        private bool RecentSpace = false;


        public AhoComparer()
        {
            InitTree();
            Nodes_1 = new HashSet<AhoNode>();
            Nodes_2 = new HashSet<AhoNode>();
            IsFirst = true;
            WaitingForStart = true;
        }

        private void InitTree()
        {
            string[] CARDINAL_WORDS = {
                "copy", "create", "createfile", "createdirectory",
                "createfiles", "createdirectories", "delete", "drop", "hide", "lock", "move",
                "open", "reaccess", "rechange", "recreate", "remodify", "rename", "select",
                "unhide", "unlock", "force", "stack", "print", "run", "sleep", "exit", "error"
            };

            string[] USUAL_WORDS = {
                "and", "or", "xor", "not", "in", "like", "else", "if",
                "inside", "times", "while", "every", "final", "limit", "order", "skip", "where", "as",
                "by", "to", "extensionless", "with", "asc", "desc", "break", "continue" };

            string[] TIME_WORDS = {
                "january", "february", "march", "april", "may", "june", 
                "july", "august", "september", "october", "november", "december",
                "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday",
                "year", "month", "week", "weekday", "date", "day", "hour", "minute", "second",
                "years", "months", "weeks", "days", "hours", "minutes", "seconds" };

            string[] VARIABLE_WORDS = new string[] {
                "now", "today", "yesterday", "tomorrow", "desktop", "perun2", "origin", "alphabet", "ascii", "arguments",
                "files", "recursivefiles", "directories", "recursivedirectories", "index", "this", "location", "success",
                "archive", "compressed", "empty", "encrypted", "exists", "hidden", "isdirectory", "isfile", "readonly",
                "access", "change", "creation", "modification", "lifetime", "size", "depth", "nothing", "never",
                "drive", "extension", "fullname", "name", "parent", "path", "true", "false",
                "pendrive", "pendrives", "downloads", "nan" }.Add(ConstantsKeywords.PROGRAMS).ToLower();
            
            Tree = new AhoTree();
            Tree.AddWords(CARDINAL_WORDS, WordType.Cardinal);
            Tree.AddWords(USUAL_WORDS, WordType.Usual);
            Tree.AddWords(TIME_WORDS, WordType.Time);
            Tree.AddWords(VARIABLE_WORDS, WordType.Variable);
        }

        public int GetRecentWordLength()
        {
            return RecentWordLength;
        }

        public WordType GetRecentWordType()
        {
            return RecentWordType;
        }

        public bool WasRecentSpace()
        {
            return RecentSpace;
        }

        public void Reset()
        {
            Nodes_1.Clear();
            Nodes_2.Clear();
            IsFirst = true;
            WaitingForStart = true;
        }

        public void Next(char ch)
        {
            HashSet<AhoNode> currNodes = IsFirst ? Nodes_1 : Nodes_2;
            HashSet<AhoNode> prevNodes = IsFirst ? Nodes_2 : Nodes_1;

            if (Char.IsLetter(ch) || Char.IsDigit(ch))
            {
                if (WaitingForStart)
                {
                    AhoNode nodeStart = Tree.GetRootStartingWith(ch);
                    if (nodeStart != null)
                    {
                        currNodes.Add(nodeStart);
                    }

                    WaitingForStart = false;
                }

                
                foreach (AhoNode node in prevNodes)
                {
                    AhoNode nodeContinue = node.GetNodeWith(ch);
                    if (nodeContinue != null)
                    {
                        currNodes.Add(nodeContinue);
                    }
                }
            }
            else
            {
                currNodes.Clear();
                WaitingForStart = true;
            }

            prevNodes.Clear();
            IsFirst = !IsFirst;
        }


        public bool FindWordEnd()
        {
            HashSet<AhoNode> currNodes = IsFirst ? Nodes_2 : Nodes_1;
            bool found = false;

            foreach (AhoNode node in currNodes)
            {
                if (node.EndsAnyWord())
                {
                    found = true;
                    RecentWordLength = node.GetDepth();
                    RecentWordType = node.GetWordEndType();
                }
            }

            return found;
        }

    }
}
