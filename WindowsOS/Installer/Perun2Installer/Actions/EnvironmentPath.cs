using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer.Actions
{
    class EnvironmentPath : Action
    {
        public override bool Do()
        {
            try
            {
                EnvVariable.AddToPath(Paths.GetInstance().GetInstallationPath());
            }
            catch (Exception)
            {
                // this action can fail
                // is not essential for installation
            }
            return true;
        }

        public override void Undo()
        {
            try
            {
                EnvVariable.DeleteFromPath(Paths.GetInstance().GetInstallationPath());
            }
            catch (Exception) { }
        }
    }
}
