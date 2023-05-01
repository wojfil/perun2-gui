using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Uninstaller.Actions
{
    class DeleteEnvironmentPath : Action
    {
        public override bool Do()
        {
            try
            {
                EnvVariable.DeleteFromPath(Paths.GetInstance().GetRootPath());
            }
            catch (Exception) { }

            return true;
        }
    }
}
