using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Uninstaller.Actions
{
    class ActionChain
    {
        private List<Action> Actions = new List<Action>();

        public ActionChain()
        {
            Actions.Add(new DeleteExecutables());
            Actions.Add(new DeleteMostOfFiles());
            Actions.Add(new DeleteEnvironmentPath());
            Actions.Add(new DeleteDesktopShortcut());
            Actions.Add(new DeleteMenuStartShortcut());
            Actions.Add(new DeleteRegistryKeys());
        }

        // run uninstall actions sequentially one by one
        public bool Run()
        {
            foreach (var action in Actions)
            {
                try
                {
                    if (!action.Do())
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Popup.Error("Something wrong happened and Perun2 could not be uninstalled. Try again. Error message: " + e.Message);
                    return false;
                }
            }

            return true;
        }
    }
}
