using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer.Actions
{
    class ActionChain
    {
        public bool isActualization = false;
        private List<Action> Actions = new List<Action>();

        public ActionChain()
        {
            this.InitInstallation();
        }

        public void InitInstallation()
        {
            Actions.Clear();
            Actions.Add(new CreateMainDir());
            Actions.Add(new CreateSubdirs());
            Actions.Add(new UnloadGlobalScripts());
            Actions.Add(new UnloadInstallationFiles());
            Actions.Add(new EnvironmentPath());
            Actions.Add(new ExtensionAssociation());
            Actions.Add(new Registry_RunPerun2Now());
            Actions.Add(new Registry_GlobalScripts());
            Actions.Add(new Registry_DirectoryHere());
            Actions.Add(new Registry_FolderHere());
            Actions.Add(new Registry_DirectoryDropdown());
            Actions.Add(new Registry_FolderDropdown());
            Actions.Add(new Registry_Uninstaller());
        }

        public void InitActualization()
        {
            Actions.Clear();
            Actions.Add(new UnloadInstallationFiles());
        }


        // run installation actions sequentially one by one
        // if installation failed, undo changes
        public bool Run()
        {
            bool broken = false;

            try
            {
                foreach (var action in Actions)
                {
                    bool success = action.Do();
                    if (success)
                    {
                        action.MarkAsFinished();
                    }
                    else
                    {
                        broken = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                broken = true;
            }

            if (broken)
            {
                CleanUp();
            }

            return !broken;
        }

        public void CleanUp()
        {
            // if installation failed
            // undo changes
            // from the last to the first
            for (int i = Actions.Count - 1; i >= 0; i--)
            {
                Action action = Actions[i];

                if (action.IsFinished())
                {
                    action.Undo();
                }
            }
        }
    }
}
