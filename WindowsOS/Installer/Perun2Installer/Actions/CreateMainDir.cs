using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Installer.Actions
{
    class CreateMainDir : Action
    {
        public override bool Do()
        {
            try
            {
                string path = Paths.GetInstance().GetInstallationPath();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
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
                string path = Paths.GetInstance().GetInstallationPath();

                if (!Directory.EnumerateFiles(path).Any()
                    && !Directory.EnumerateDirectories(path).Any())
                {
                    Directory.Delete(path);
                }
            }
            catch (Exception) { }
        }
    }
}
