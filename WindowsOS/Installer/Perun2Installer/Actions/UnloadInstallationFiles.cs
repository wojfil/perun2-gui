using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
                string actualize = Path.Combine(intallation, Constants.FILE_ACTUALIZE);

                Create(gui, Properties.Resources.Perun2Gui);
                Create(manager, Properties.Resources.Perun2Manager);
                Create(perun2, Properties.Resources.perun2);
                Create(uninstall, Properties.Resources.uninstall);
                CreateTextFile(settings, GetDefaultSettings());
                CreateTextFile(actualize, GetActualizeBatch());

                if (System.IO.File.Exists(icon))
                {
                    System.IO.File.Delete(icon);
                }
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

        private void CreateTextFile(string path, string value)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            System.IO.File.WriteAllText(path, value);
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

        private string GetActualizeBatch()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("IF EXIST \"perun2.exe\" DEL \"perun2.exe\"");
            sb.AppendLine("IF EXIST \"uninstall.exe\" DEL \"uninstall.exe\"");
            sb.AppendLine("IF EXIST \"Perun2 Gui.exe\" DEL \"Perun2 Gui.exe\"");
            sb.AppendLine("IF EXIST \"Perun2 Manager.exe\" DEL \"Perun2 Manager.exe\"");
            sb.AppendLine("rename \"newperun2.exe\" \"perun2.exe\"");
            sb.AppendLine("rename \"newuninstall.exe\" \"uninstall.exe\"");
            sb.AppendLine("rename \"newPerun2_Gui.exe\" \"Perun2 Gui.exe\"");
            sb.AppendLine("rename \"newPerun2_Manager.exe\" \"Perun2 Manager.exe\"");
            sb.AppendLine("start \"\" \"Perun2 Gui.exe\"");
            
            return sb.ToString();
        }
    }
}
