using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Installer.Actions
{
    class CreateSubdirs : Action
    {
        public override bool Do()
        {
            try
            {
                string backups = Paths.GetInstance().GetBackupsPath();
                if (!Directory.Exists(backups))
                {
                    Directory.CreateDirectory(backups);
                }

                string scripts = Paths.GetInstance().GetScriptsPath();
                if (!Directory.Exists(scripts))
                {
                    Directory.CreateDirectory(scripts);
                }

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override void Undo()
        {
            try
            {
                string backups = Paths.GetInstance().GetBackupsPath();
                if (!Directory.Exists(backups))
                {
                    Directory.Delete(backups);
                }

                string scripts = Paths.GetInstance().GetScriptsPath();
                if (!Directory.Exists(scripts))
                {
                    Directory.Delete(scripts);
                }
            }
            catch (Exception) { }
        }
    }
}
