using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Installer
{
    public class Paths
    {
        private static Paths INSTANCE = new Paths();

        private string RootPath = "";


        private Paths()
        {

        }

        public void ErasePath()
        {
            RootPath = "";
        }

        public void SetPath(string path)
        {
            RootPath = path;
        }

        public static Paths GetInstance()
        {
            return INSTANCE;
        }

        public bool HasPath()
        {
            return RootPath.Length != 0;
        }

        public string GetRootPath()
        {
            return RootPath;
        }

        public bool RootExists()
        {
            return HasPath() && Directory.Exists(RootPath);
        }

        public string GetInstallationPath()
        {
            return Path.Combine(RootPath, Constants.FOLDER_MAIN);
        }

        public string GetScriptsPath()
        {
            return Path.Combine(GetInstallationPath(), Constants.FOLDER_SCRIPTS);
        }

        public string GetIconPath()
        {
            return Path.Combine(GetInstallationPath(), Constants.FILE_ICON);
        }

        public string GetUninstallPath()
        {
            return Path.Combine(GetInstallationPath(), Constants.FILE_UNINSTALL);
        }

        public string GetPerun2Path()
        {
            return Path.Combine(GetInstallationPath(), Constants.FILE_PERUN2);
        }

        public string GetGuiPath()
        {
            return Path.Combine(GetInstallationPath(), Constants.FILE_GUI);
        }

        public string GetManagerPath()
        {
            return Path.Combine(GetInstallationPath(), Constants.FILE_MANANGER);
        }

        public string GetBackupsPath()
        {
            return Path.Combine(GetInstallationPath(), Constants.FOLDER_BACKUPS);
        }



    }

}
