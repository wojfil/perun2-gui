using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Uninstaller
{
    public class Paths
    {
        private static Paths INSTANCE = new Paths();

        private string RootPath = "";


        private Paths()
        {
            RootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
        }

        public static Paths GetInstance()
        {
            return INSTANCE;
        }

        public string GetRootPath()
        {
            return RootPath;
        }

        public string GetScriptsPath()
        {
            return Path.Combine(RootPath, Constants.FOLDER_SCRIPTS);
        }

        public string GetIconPath()
        {
            return Path.Combine(RootPath, Constants.FILE_ICON);
        }

        public string GetPerun2Path()
        {
            return Path.Combine(RootPath, Constants.FILE_PERUN2);
        }

        public string GetGuiPath()
        {
            return Path.Combine(RootPath, Constants.FILE_GUI);
        }

        public string GetManagerPath()
        {
            return Path.Combine(RootPath, Constants.FILE_MANANGER);
        }

        public string GetBackupsPath()
        {
            return Path.Combine(RootPath, Constants.FOLDER_BACKUPS);
        }
    }
}
