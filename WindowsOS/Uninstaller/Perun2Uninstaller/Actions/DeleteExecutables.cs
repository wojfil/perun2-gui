using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Uninstaller.Actions
{
    class DeleteExecutables : Action
    {
        // delete all executable files except for the "uninstall.exe"
        public override bool Do()
        {
             try
             {
                 DeleteIfExists(Paths.GetInstance().GetPerun2Path());
                 DeleteIfExists(Paths.GetInstance().GetManagerPath());
                 DeleteIfExists(Paths.GetInstance().GetGuiPath());
             }
             catch (Exception)
             {
                 Popup.Error("You have to close all running Perun2 instances first.");
                 return false;
             }

            return true;
         }

        private void DeleteIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
