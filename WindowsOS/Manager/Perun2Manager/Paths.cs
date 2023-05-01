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
using System.IO;

namespace Perun2Manager
{
    public sealed class Paths
    {
        private static Paths Instance;

        private readonly string BasePath;    // absolute path to the place where Perun2 is installed 
        private readonly string ScriptsPath; // absolute path to the directory with global scripts 


        private Paths()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8);
            BasePath = Path.GetDirectoryName(path);
            ScriptsPath = Path.Combine(BasePath, Constants.DIRNAME_SCRIPTS);
        }

        public string GetBasePath()
        {
            return BasePath;
        }

        public string GetScriptsPath()
        {
            return ScriptsPath;
        }

        public string GetGUIPath()
        {
            return Path.Combine(BasePath, Constants.FILENAME_GUI);
        }

        public string GetExePath()
        {
            return Path.Combine(BasePath, Constants.FILENAME_EXE);
        }

        public string GetScriptPath(string name)
        {
            return Path.Combine(ScriptsPath, name + Constants.PERUN2_EXTENSION);
        }

        public static Paths GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Paths();
            }
            return Instance;
        }

    }
}
