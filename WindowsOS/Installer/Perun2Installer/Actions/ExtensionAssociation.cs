using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Perun2Installer.Actions
{
    class ExtensionAssociation : Action
    {
        public override bool Do()
        {
            string intallation = Paths.GetInstance().GetInstallationPath();
            string gui = Path.Combine(intallation, Constants.FILE_GUI);
            FileAssociations.SetAssociation(Constants.PERU_EXTENSION, "Perun2", "Perun2 Script", gui);
            return true;
        }

        public override void Undo()
        {
            //
        }
    }
}
