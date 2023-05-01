using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer.Actions
{
    class Registry_FolderHere : Action
    {
        private static readonly string HereMenu = "Folder\\shell\\Perun2Here";
        private static readonly string HereCommand = "Folder\\shell\\Perun2Here\\command";

        public override bool Do()
        {
            string icon = Paths.GetInstance().GetIconPath();
            string gui = Paths.GetInstance().GetGuiPath();

            List<Tuple<string, string>> perunMenuValues = new List<Tuple<string, string>>()
            {
               Tuple.Create("",  Constants.PERUN2_HERE),
               Tuple.Create("MUIVerb",  Constants.PERUN2_HERE),
               Tuple.Create("Icon",  icon.Quoted())
            };

            List<Tuple<string, string>> perunCommandValues = new List<Tuple<string, string>>()
            {
               Tuple.Create("",   "\"" + gui + "\" \"%V\"")
            };

            RegistryAction.AddKey(HereMenu, perunMenuValues);
            RegistryAction.AddKey(HereCommand, perunCommandValues);

            return true;
        }

        public override void Undo()
        {
            RegistryAction.RemoveKey(HereCommand);
            RegistryAction.RemoveKey(HereMenu);
        }
    }
}
