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
using System.Net;

namespace Perun2Gui
{
    public class Connection
    {
        private static Connection Instance = new Connection();
        private bool Initialized;

        public Connection()
        {
            Initialized = false;
        }

        public static Connection GetInstance()
        {
            return Instance;
        }

        public void Init()
        {
            if (!Initialized)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                Initialized = true;
            }
        }
    }
}
