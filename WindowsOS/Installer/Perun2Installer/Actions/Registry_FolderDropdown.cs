using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer.Actions
{
    class Registry_FolderDropdown : Action
    {
        private static readonly string DropdownMenu = "Folder\\shell\\Perun2";

        public override bool Do()
        {
            string icon = Paths.GetInstance().GetIconPath();
            string gui = Paths.GetInstance().GetGuiPath();

            List<Tuple<string, string>> dropdownValues = new List<Tuple<string, string>>()
            {
               Tuple.Create("",  Constants.PERUN2),
               Tuple.Create("MUIVerb",  Constants.PERUN2),
               Tuple.Create("Icon",  icon.Quoted()),
               Tuple.Create("ExtendedSubCommandsKey", "*\\Perun2\\GlobalScripts")
            };

            RegistryAction.AddKey(DropdownMenu, dropdownValues);
            return true;
        }

        public override void Undo()
        {
            RegistryAction.RemoveKey(DropdownMenu);
        }
    }
}
