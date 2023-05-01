using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Uninstaller.Actions
{
    class DeleteMostOfFiles : Action
    {
        // delete all directories and files except for the "uninstall.exe"
        public override bool Do()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Paths.GetInstance().GetRootPath());

            foreach (FileInfo file in di.GetFiles())
            {
                if (!file.Name.Equals(Constants.FILE_UNINSTALL))
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                        Popup.Error("File '" + file.Name + "' is currently used by another process. You have to close it first.");
                        return false;
                    }
                }
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                }
                catch (Exception)
                {
                    Popup.Error("Content of directory '" + dir.Name + "' is currently used by another process. You have to close it first.");
                    return false;
                }
            }

            return true;
        }
    }
}
