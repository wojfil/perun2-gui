using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Uninstaller.Actions
{
    class DeleteMenuStartShortcut : Action
    {
        public override bool Do()
        {
            try
            {
                string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
                string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", Constants.PERUN2);

                if (Directory.Exists(appStartMenuPath))
                {
                    string shortcutLocation = Path.Combine(appStartMenuPath, Constants.SHORTCUT_NAME);

                    if (File.Exists(shortcutLocation))
                    {
                        File.Delete(shortcutLocation);
                    }
                    Directory.Delete(appStartMenuPath);
                }

            }
            catch (Exception) { }

            return true;
        }
    }
}
