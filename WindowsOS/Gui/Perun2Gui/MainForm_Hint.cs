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
using System.Windows.Forms;
using Perun2Gui.Hint;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        private bool HasHint;
        private string Hint;
        private HintType RecentHintType;
        private char CharAfterHint;

        private string HintPrevWord;
        private string HintCurrentWord;
        private HintGenerator HintGenerator = new HintGenerator();


        private void RefreshHint()
        {
            if (!IsProperHint())
            {
                SetNullHint();
                return;
            }

            LoadHintPrevWords();

            if (HintGenerator.Matches(HintPrevWord, HintCurrentWord))
            {
                SetHint();
            }
            else
            {
                SetNullHint();
            }
        }

        private void SetNullHint()
        {
            HasHint = false;
            hintLabel.Text = "";
        }

        private bool IsProperHint()
        {
            if (codeBox.SelectionLength != 0)
            {
                return false;
            }

            int index = codeBox.SelectionStart;
            int length = codeBox.Text.Length;
            bool atEnd = index == length;

            if (atEnd)
            {
                CharAfterHint = ' ';
            }
            else
            {
                CharAfterHint = codeBox.Text[index];
                if (Char.IsLetterOrDigit(CharAfterHint) || CharAfterHint.Equals('_'))
                {
                    return false;
                }
            }

            //check if Caret is Inside Comment Or String Literal
            HighlightMode mode = HighlightMode.Normal;
            char prev = ' ';
            for (int i = 0; i < index; i++)
            {
                char ch = codeBox.Text[i];
                switch (mode)
                {
                    case HighlightMode.Normal:
                    {
                        if (ch == '\'')
                        {
                            mode = HighlightMode.StringA;
                        }
                        else if (ch == '`')
                        {
                            mode = HighlightMode.StringB;
                        }
                        else if (prev == '/' && ch == '/')
                        {
                            mode = HighlightMode.CommentSingle;
                        }
                         else if (prev == '/' && ch == '*')
                        {
                            mode = HighlightMode.CommentMulti;
                        }
                        break;
                    }
                    case HighlightMode.StringA:
                    {
                        if (ch == '\'')
                        {
                            mode = HighlightMode.Normal;
                        }
                        break;
                    }

                    case HighlightMode.StringB:
                    {
                        if (ch == '`')
                        {
                            mode = HighlightMode.Normal;
                        }
                        break;
                    }
                    case HighlightMode.CommentSingle:
                    {
                        if (ch.IsNewLine())
                        {
                            mode = HighlightMode.Normal;
                        }
                        break;
                    }
                    case HighlightMode.CommentMulti:
                    {
                        if (prev == '*' && ch == '/')
                        {
                            mode = HighlightMode.Normal;
                        }
                        break;
                    }
                }
                prev = ch;
            }

            if (mode != HighlightMode.Normal)
            {
                return false;
            }

            if (atEnd)
            {
                return true;
            }

            char nextChar = codeBox.Text[index];

            if (index != 0 && codeBox.Text[index - 1] == '/')
            {
                if (nextChar == '*' || nextChar == '/')
                {
                    return false;
                }
            }

            return true;
        }

        private void LoadHintPrevWords()
        {
            int index = codeBox.SelectionStart;
            string code = codeBox.Text;
            int length = code.Length;

            if (index == 0)
            {
                HintCurrentWord = String.Empty;
                HintPrevWord = String.Empty;
                return;
            }

            int i = index - 1;
            char ch = code[i];

            if (ch.IsSpace())
            {
                HintCurrentWord = String.Empty;
            }
            else if (ch.IsPerun2WordChar())
            {
                i--;
                for (; i >= 0; i--)
                {
                    ch = code[i];

                    if (ch.IsSpace())
                    {
                        HintCurrentWord = code.Substring(i + 1, index - i - 1);
                        break;
                    }
                    else if (!ch.IsPerun2WordChar())
                    {
                        HintCurrentWord = code.Substring(i + 1, index - i - 1);
                        HintPrevWord = ch.ToString();
                        return;
                    }
                }
                if (i == -1)
                {
                    HintCurrentWord = code.Substring(0, index);
                    HintPrevWord = String.Empty;
                    return;
                }
            }
            else
            {
                HintCurrentWord = String.Empty;
                HintPrevWord = ch.ToString();
                return;
            }

            for (; i >= 0; i--)
            {
                ch = code[i];
                if (ch.IsPerun2WordChar())
                {
                    break;
                }
                else if (!ch.IsSpace())
                {
                    HintPrevWord = ch.ToString();
                    return;
                }
            }

            if (i == -1)
            {
                HintPrevWord = String.Empty;
                return;
            }

            int e = i;

            for (; i >= 0; i--)
            {
                ch = code[i];
                if (ch.IsSpace() || !ch.IsPerun2WordChar())
                {
                    HintPrevWord = code.Substring(i + 1, e - i);
                    return;
                }
                else if (!ch.IsPerun2WordChar())
                {
                    HintPrevWord = ch.ToString();
                    return;
                }
            }

            HintPrevWord = code.Substring(0, e + 1);
            return;
        }

        private void SetHint()
        {
            HasHint = true;
            Hint = HintGenerator.GetRecentValue();
            RecentHintType = HintGenerator.GetRecentHintType();
            int startLength = HintCurrentWord.Length;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < startLength; i++)
            {
                char ch = HintCurrentWord[i];
                sb.Append(ch + char.ConvertFromUtf32(0x359));
            }

            sb.Append(Hint.Substring(startLength));
            hintLabel.Text = sb.ToString();
        }

        private void InsertWordFromHint()
        {
            int caret = codeBox.SelectionStart;
            int startLength = HintCurrentWord.Length;
            LockedTextChange = true;

            string left = codeBox.Text.Substring(0, caret);
            string right = codeBox.Text.Substring(caret);
            bool HintRetreat = RecentHintType == HintType.Retreating;
            bool rightSpace = !HintRetreat && !right.StartsWith(" ");
            string hintWord;
            int newCaret;

            if (CharAfterHint == '(' && Hint.EndsWith(")"))
            {
                hintWord = Hint.Substring(startLength);
                hintWord = hintWord.Substring(0, hintWord.Length - 2);
                newCaret = caret + hintWord.Length + 1;
            }
            else {
                hintWord = Hint.Substring(startLength) + (rightSpace ? " " : "");
                newCaret = caret + hintWord.Length - (HintRetreat ? 1 : 0);
            }

            codeBox.Text = left + hintWord + right;
            codeBox.SelectionStart = newCaret;

            LockedTextChange = false;
            History.AddUnit();
            RefreshHistoryMenuItems();
            RefreshHint();
        }
    }
}
