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
using FastColoredTextBoxNS;

namespace Perun2Gui
{
    class History
    {
        private FastColoredTextBox TextBox;
        private List<HistoryUnit> Units;
        private int Index;

        public History(FastColoredTextBox textBox)
        {
            TextBox = textBox;
            Units = new List<HistoryUnit>();
            Clear();
        }

        public void Clear()
        {
            Units.Clear();
            Units.Add(new HistoryUnit(String.Empty, 0, 0));
            Index = 0;
        }

        public int GetIndex()
        {
            return Index;
        }

        public bool HasPrevious()
        {
            return Index > 0;
        }

        public bool HasNext()
        {
            return Index != Units.Count() - 1;
        }

        private void RefreshTextBox()
        {
            HistoryUnit unit = Units[Index];
            TextBox.Text = unit.GetCode();
            TextBox.SelectionStart = unit.GetStart();
            TextBox.SelectionLength = unit.GetLength();
        }

        public void GoForward()
        {
            if (HasNext())
            {
                Index++;
                RefreshTextBox();
            }
        }

        public void GoBackward()
        {
            if (HasPrevious())
            {
                Index--;
                RefreshTextBox();
            }
        }

        public void AddUnit()
        {
            /*if (HasPrevious() && Units[Index - 1].GetCode().Equals(TextBox.Text))
            {
                return;
            }*/

            HistoryUnit unit = new HistoryUnit(TextBox.Text, TextBox.SelectionStart, TextBox.SelectionLength);
            if (HasNext())
            {
                int count = Units.Count();
                Units.RemoveRange(Index + 1, count - Index - 1);
            }

            Index++;
            Units.Add(unit);
        }

        public void SaveCurrentSelection()
        {
            Units[Index].SetSelection(TextBox.SelectionStart, TextBox.SelectionLength);
        }

        public void StartWithCode(string code)
        {
            Units.Clear();
            Units.Add(new HistoryUnit(code, 0, 0));
            Index = 0;
        }
        
    }
}
