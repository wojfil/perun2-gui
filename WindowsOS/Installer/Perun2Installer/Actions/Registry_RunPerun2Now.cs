using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer.Actions
{
    class Registry_RunPerun2Now : Action
    {
        private static readonly string PerunMenu = "Perun2\\shell\\RunPerun2Now";
        private static readonly string PerunCommand = "Perun2\\shell\\RunPerun2Now\\command";

        public override bool Do()
        {
            string icon = Paths.GetInstance().GetIconPath();
            string perun = Paths.GetInstance().GetPerun2Path();

            List<Tuple<string, string>> perunMenuValues = new List<Tuple<string, string>>()
            {
               Tuple.Create("",  Constants.PERUN2_RUN_NOW),
               Tuple.Create("MUIVerb",  Constants.PERUN2_RUN_NOW),
               Tuple.Create("Icon",  icon.Quoted())
            };

            List<Tuple<string, string>> perunCommandValues = new List<Tuple<string, string>>()
            {
               Tuple.Create("",   "\"" + perun + "\" -s \"%1\"")
            };

            RegistryAction.AddKey(PerunMenu, perunMenuValues);
            RegistryAction.AddKey(PerunCommand, perunCommandValues);

            return true;
        }

        public override void Undo()
        {
            RegistryAction.RemoveKey(PerunCommand);
            RegistryAction.RemoveKey(PerunMenu);
        }
    }
}
