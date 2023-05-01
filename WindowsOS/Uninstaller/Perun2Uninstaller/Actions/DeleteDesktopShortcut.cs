using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Uninstaller.Actions
{
    class DeleteDesktopShortcut : Action
    {
        public override bool Do()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string location = Path.Combine(path, Constants.SHORTCUT_NAME);

                if (File.Exists(location))
                {
                    File.Delete(location);
                }
            }
            catch (Exception) { }

            return true;
        }
    }
}
