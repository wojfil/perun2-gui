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
using FastColoredTextBoxNS;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {

        private static readonly Style CommentStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        private static readonly Style CardinalStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold);
        private static readonly Style UsualStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        private static readonly Style InnerVariablesStyle = new TextStyle(Brushes.DarkViolet, null, FontStyle.Regular);
        private static readonly Style StringStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        private static readonly Style TimeStyle = new TextStyle(Brushes.Crimson, null, FontStyle.Regular);

        private AhoComparer Comparer;


        private void InitAho()
        {
            Comparer = new AhoComparer();
        }


        private void RefreshBoxGraphics(FastColoredTextBox textBox)
        {
            Range allRange = textBox.Range;
            allRange.ClearStyle(CardinalStyle);
            allRange.ClearStyle(UsualStyle);
            allRange.ClearStyle(InnerVariablesStyle);
            allRange.ClearStyle(StringStyle);
            allRange.ClearStyle(TimeStyle);
            allRange.ClearStyle(CommentStyle);

            char prev = ' ';
            HighlightMode mode = HighlightMode.Normal;
            string code = codeBox.Text;
            int len = code.Length;
            int startCol = 0;
            int startLine = 0;
            int col = 0;
            int line = 0;
            int finalId = len - 1;
            WordType recentWordType = WordType.Null;
            bool closed = false;


            for (int i = 0; i < len; i++)
            {
                char ch = code[i];

                switch (mode)
                {
                    case HighlightMode.Normal:
                    {
                        if (ch == '\'')
                        {
                            startCol = col;
                            startLine = line;
                            mode = HighlightMode.StringA;
                            if (Comparer.FindWordEnd())
                            {
                                WordEnd(out recentWordType, ref i, col, line);
                            }
                            Comparer.Reset();
                        }
                        else if (ch == '`')
                        {
                            startCol = col;
                            startLine = line;
                            mode = HighlightMode.StringB;
                            if (Comparer.FindWordEnd())
                            {
                                WordEnd(out recentWordType, ref i, col, line);
                            }
                            Comparer.Reset();
                        }
                        else if (prev == '/' && ch == '/')
                        {
                            startCol = col;
                            startLine = line;
                            mode = HighlightMode.CommentSingle;
                            if (Comparer.FindWordEnd())
                            {
                                WordEnd(out recentWordType, ref i, col, line);
                            }
                            Comparer.Reset();
                            closed = true;
                        }
                        else if (prev == '/' && ch == '*')
                        {
                            startCol = col;
                            startLine = line;
                            mode = HighlightMode.CommentMulti;
                            Comparer.Reset();
                            closed = true;
                        }
                        else if (System.Char.IsLetterOrDigit(ch))
                        {
                            Comparer.Next(System.Char.ToLower(ch));
                        }
                        else
                        {
                            startCol = col;
                            startLine = line;
                            if (Comparer.FindWordEnd())
                            {
                                WordEnd(out recentWordType, ref i, col, line);
                            }
                            else if (!System.Char.IsWhiteSpace(ch) && ch != '/')
                            {

                            }
                            Comparer.Reset();
                        }

                        break;
                    }
                    case HighlightMode.StringA:
                    {
                        if (ch == '\'')
                        {
                            mode = HighlightMode.Normal;
                            Range rng = new Range(codeBox, startCol, startLine, col + 1, line);
                            rng.SetStyle(StringStyle);
                        }
                        break;
                    }
                    case HighlightMode.StringB:
                    {
                        if (ch == '`')
                        {
                            mode = HighlightMode.Normal;
                            Range rng = new Range(codeBox, startCol, startLine, col + 1, line);
                            rng.SetStyle(StringStyle);
                        }
                        break;
                    }
                    case HighlightMode.CommentSingle:
                    {
                        if (ch.IsNewLine())
                        {
                            mode = HighlightMode.Normal;
                            Range rng = new Range(codeBox, startCol - 1, startLine, col, line);
                            rng.SetStyle(CommentStyle);
                        }
                        break;
                    }
                    case HighlightMode.CommentMulti:
                    {
                        if (prev == '*' && ch == '/')
                        {
                            mode = HighlightMode.Normal;
                            Range rng = new Range(codeBox, startCol - 1, startLine, col + 1, line);
                            rng.SetStyle(CommentStyle);
                            closed = true;
                        }
                        break;
                    }
                }
                
                if (ch == '\n')
                {
                    line++;
                    col = 0;
                }
                else
                {
                    col++;
                }

                if (closed)
                {
                    closed = false;
                    prev = ' ';
                }
                else
                {
                    prev = ch;
                }
            }

            if (mode != HighlightMode.Normal)
            {
                bool isComment = mode == HighlightMode.CommentSingle || mode == HighlightMode.CommentMulti;
                Range rng = new Range(codeBox, startCol + (isComment ? -1 : 0), startLine, col, line);
                rng.SetStyle(isComment ? CommentStyle : StringStyle);
            }
            else
            {
                if (Comparer.FindWordEnd())
                {
                    WordEnd(out recentWordType, ref len, col, line);
                }
                Comparer.Reset();
            }
        }

        private void WordEnd(out WordType recentWordType, ref int i, int col, int line)
        {
            int wlength = Comparer.GetRecentWordLength();
            WordType wtype = Comparer.GetRecentWordType();
            SetStyle(col, line, wlength, wtype);
            recentWordType = wtype;
        }

        private void SetStyle(int endChar, int line, int length, WordType wordType)
        {
            int startChar = endChar - length;
            Range rng = new Range(codeBox, startChar, line, endChar, line);

            switch (wordType)
            {
                case WordType.Cardinal:
                {
                    rng.SetStyle(CardinalStyle);
                    break;
                }
                case WordType.Usual:
                {
                    rng.SetStyle(UsualStyle);
                    break;
                }
                case WordType.Variable:
                {
                    rng.SetStyle(InnerVariablesStyle);
                    break;
                }
                case WordType.Time:
                {
                    rng.SetStyle(TimeStyle);
                    break;
                }
            }
        }


    }
}
