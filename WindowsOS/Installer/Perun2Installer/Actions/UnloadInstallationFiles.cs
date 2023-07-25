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

                DeleteFileIfExists(gui);
                DeleteFileIfExists(manager);
                DeleteFileIfExists(perun2);
                DeleteFileIfExists(uninstall);
                DeleteFileIfExists(settings);
                DeleteFileIfExists(icon);

                Create(gui, Properties.Resources.Perun2Gui);
                Create(manager, Properties.Resources.Perun2Manager);
                Create(perun2, Properties.Resources.perun2);
                Create(uninstall, Properties.Resources.uninstall);
                CreateTextFile(settings, GetDefaultSettings());

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
