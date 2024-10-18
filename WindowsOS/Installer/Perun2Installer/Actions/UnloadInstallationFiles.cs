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

                string dll_avcodec = Path.Combine(intallation, "avcodec - 61.dll");
                string dll_avdevice = Path.Combine(intallation, "avdevice - 61.dll");
                string dll_avfilter = Path.Combine(intallation, "avfilter - 10.dll");
                string dll_avformat = Path.Combine(intallation, "avformat - 61.dll");
                string dll_avutil = Path.Combine(intallation, "avutil - 59.dll");
                string dll_postproc = Path.Combine(intallation, "postproc - 58.dll");
                string dll_swresample = Path.Combine(intallation, "swresample - 5.dll");
                string dll_swscale = Path.Combine(intallation, "swscale - 8.dll");

                DeleteFileIfExists(gui);
                DeleteFileIfExists(manager);
                DeleteFileIfExists(perun2);
                DeleteFileIfExists(uninstall);
                DeleteFileIfExists(settings);
                DeleteFileIfExists(icon);

                DeleteFileIfExists(dll_avcodec);
                DeleteFileIfExists(dll_avdevice);
                DeleteFileIfExists(dll_avfilter);
                DeleteFileIfExists(dll_avformat);
                DeleteFileIfExists(dll_avutil);
                DeleteFileIfExists(dll_postproc);
                DeleteFileIfExists(dll_swresample);
                DeleteFileIfExists(dll_swscale);

                Create(gui, Properties.Resources.Perun2Gui);
                Create(manager, Properties.Resources.Perun2Manager);
                Create(perun2, Properties.Resources.perun2);
                Create(uninstall, Properties.Resources.uninstall);
                CreateTextFile(settings, GetDefaultSettings());

                Create(dll_avcodec, Properties.Resources.avcodec_61);
                Create(dll_avdevice, Properties.Resources.avdevice_61);
                Create(dll_avfilter, Properties.Resources.avfilter_10);
                Create(dll_avformat, Properties.Resources.avformat_61);
                Create(dll_avutil, Properties.Resources.avutil_59);
                Create(dll_postproc, Properties.Resources.postproc_58);
                Create(dll_swresample, Properties.Resources.swresample_5);
                Create(dll_swscale, Properties.Resources.swscale_8);

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
