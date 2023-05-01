using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer.Actions
{
    class Registry_GlobalScripts : Action
    {
        private static readonly string Path_1 = "*\\Perun2";
        private static readonly string Path_2 = "*\\Perun2\\GlobalScripts";
        private static readonly string Path_3 = "*\\Perun2\\GlobalScripts\\shell";

        private static readonly string Path_Add = "*\\Perun2\\GlobalScripts\\shell\\v_Add";
        private static readonly string Path_Add_Command = "*\\Perun2\\GlobalScripts\\shell\\v_Add\\command";

        public override bool Do()
        {
            string icon = Paths.GetInstance().GetIconPath();

            List<Tuple<string, string>> emptyValues = new List<Tuple<string, string>>()
            {
               Tuple.Create("",  "")
            };

            RegistryAction.AddKey(Path_1, emptyValues);
            RegistryAction.AddKey(Path_2, emptyValues);
            RegistryAction.AddKey(Path_3, emptyValues);

            List<Tuple<string, string>> addValues = new List<Tuple<string, string>>()
            {
               Tuple.Create("",  ""),
               Tuple.Create("MUIVerb",  "Global scripts..."),
               Tuple.Create("Icon",  Paths.GetInstance().GetIconPath().Quoted())
            };

            List<Tuple<string, string>> commandValues = new List<Tuple<string, string>>()
            {
               Tuple.Create("",  Paths.GetInstance().GetManagerPath().Quoted())
            };

            RegistryAction.AddKey(Path_Add, addValues);
            RegistryAction.AddKey(Path_Add_Command, commandValues);
            RegistryAction.AddDwordValue(Path_Add, "CommandFlags", 32); // add separator

            return true;
        }

        public override void Undo()
        {
            RegistryAction.RemoveKey(Path_Add_Command);
            RegistryAction.RemoveKey(Path_Add);
            RegistryAction.RemoveKey(Path_3);
            RegistryAction.RemoveKey(Path_2);
            RegistryAction.RemoveKey(Path_1);
        }
    }
}
