using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Perun2Installer.Actions
{
    class UnloadInstallationFiles : UnloadAction
    {
        public override bool Do()
        {
            try
            {
                string intallation = Paths.GetInstance().GetInstallationPath();

                string gui = Path.Combine(intallation, Constants.FILE_GUI);
                string manager = Path.Combine(intallation, Constants.FILE_MANANGER);
                string perun2 = Path.Combine(intallation, Constants.FILE_PERUN2);
                string uninstall = Path.Combine(intallation, Constants.FILE_UNINSTALL);
                string settings = Path.Combine(intallation, Constants.FILE_SETTINGS);
                string icon = Path.Combine(intallation, Constants.FILE_ICON);

                string py_analyzer = Path.Combine(intallation, Constants.PYTHON_FILE_ANALYZER);
                string py_asker = Path.Combine(intallation, Constants.PYTHON_FILE_ASKER);

                string dll_avcodec = Path.Combine(intallation, "avcodec-62.dll");
                string dll_avformat = Path.Combine(intallation, "avformat-62.dll");
                string dll_avutil = Path.Combine(intallation, "avutil-60.dll");
                string dll_icudt78 = Path.Combine(intallation, "icudt78.dll");
                string dll_icuin78 = Path.Combine(intallation, "icuin78.dll");
                string dll_icuuc78 = Path.Combine(intallation, "icuuc78.dll");
                string dll_swresample = Path.Combine(intallation, "swresample-6.dll");

                DeleteFileIfExists(gui);
                DeleteFileIfExists(manager);
                DeleteFileIfExists(perun2);
                DeleteFileIfExists(uninstall);
                DeleteFileIfExists(settings);
                DeleteFileIfExists(icon);

                DeleteFileIfExists(py_analyzer);
                DeleteFileIfExists(py_asker);

                DeleteFileIfExists(dll_avcodec);
                DeleteFileIfExists(dll_avformat);
                DeleteFileIfExists(dll_avutil);
                DeleteFileIfExists(dll_icudt78);
                DeleteFileIfExists(dll_icuin78);
                DeleteFileIfExists(dll_icuuc78);
                DeleteFileIfExists(dll_swresample);

                Create(gui, Properties.Resources.Perun2Gui);
                Create(manager, Properties.Resources.Perun2Manager);
                Create(perun2, Properties.Resources.perun2);
                Create(uninstall, Properties.Resources.uninstall);
                CreateTextFile(settings, GetDefaultSettings());

                Create(py_analyzer, Properties.Resources.analyzer);
                Create(py_asker, Properties.Resources.asker);

                Create(dll_avcodec, Properties.Resources.avcodec_62);
                Create(dll_avformat, Properties.Resources.avformat_62);
                Create(dll_avutil, Properties.Resources.avutil_60);
                Create(dll_icudt78, Properties.Resources.icudt78);
                Create(dll_icuin78, Properties.Resources.icuin78);
                Create(dll_icuuc78, Properties.Resources.icuuc78);
                Create(dll_swresample, Properties.Resources.swresample_6);

                using (FileStream fs = new FileStream(icon, FileMode.Create))
                {
                    Properties.Resources.perun256.Save(fs);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void CreateTextFile(string path, string value)
        {
            File.WriteAllText(path, value);
        }

        public override void Undo()
        {
            try
            {
                string intallation = Paths.GetInstance().GetInstallationPath();

                string gui = Path.Combine(intallation, Constants.FILE_GUI);
                string manager = Path.Combine(intallation, Constants.FILE_MANANGER);
                string perun = Path.Combine(intallation, Constants.FILE_PERUN2);
                string uninstall = Path.Combine(intallation, Constants.FILE_UNINSTALL);
                string settings = Path.Combine(intallation, Constants.FILE_SETTINGS);
                string icon = Path.Combine(intallation, Constants.FILE_ICON);
                string actualize = Path.Combine(intallation, Constants.FILE_ACTUALIZE);

                Delete(gui);
                Delete(manager);
                Delete(perun);
                Delete(uninstall);
                Delete(settings);
                Delete(icon);
                Delete(actualize);
            }
            catch (Exception) { }
        }

        private string GetDefaultSettings()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Day");
            sb.AppendLine("GPL 3");
            sb.AppendLine("English");
            sb.AppendLine("Omit");

            return sb.ToString();
        }
    }
}
