using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Perun2Gui
{
    public static class Filesystem
    {

        public static List<string> GetGlobalScriptFiles()
        {
            return Directory.EnumerateFiles(Paths.GetInstance().GetScriptsPath(), "*" + Constants.PERUN2_EXTENSION)
                .OrderBy(d => new FileInfo(d).CreationTime)
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .ToList();
        }
    }
}
