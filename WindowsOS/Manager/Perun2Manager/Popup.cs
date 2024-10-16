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

namespace Perun2Manager
{
    public static class Popup
    {
        public static void Error(string message)
        {
            Perun2Manager.Messenger.MessageBox(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Ok(string message)
        {
            Perun2Manager.Messenger.MessageBox(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
