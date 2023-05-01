using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Installer.Actions
{
    class UnloadGlobalScripts : UnloadAction
    {
        public override bool Do()
        {
            try
            {
                string scripts = Paths.GetInstance().GetScriptsPath();
                string selectAll = Path.Combine(scripts, Constants.SCRIPT_SELECT_ALL);
                string deleteDirs = Path.Combine(scripts, Constants.SCRIPT_DELETE_EMPTY_DIRECTORIES);

                Create(selectAll, Properties.Resources.Select_all);
                Create(deleteDirs, Properties.Resources.Delete_empty_directories);
            }
            catch (Exception)
            {
                // this action is not essential, so do nothing in case of failure
            }

            return true;
        }

        public override void Undo()
        {
            try
            {
                string scripts = Paths.GetInstance().GetScriptsPath();
                string selectAll = Path.Combine(scripts, Constants.SCRIPT_SELECT_ALL);
                string deleteDirs = Path.Combine(scripts, Constants.SCRIPT_DELETE_EMPTY_DIRECTORIES);

                Delete(selectAll);
                Delete(deleteDirs);
            }
            catch (Exception) { }
        }
    }
}
