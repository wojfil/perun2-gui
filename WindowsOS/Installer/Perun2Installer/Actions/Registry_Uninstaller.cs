using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Perun2Installer.Actions
{
    class Registry_Uninstaller : Action
    {
        private static readonly string UninstallRegistry = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Perun2";
        private static readonly string UninstallRegistry32on64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Perun2";

        public override bool Do()
        {
            string v = MainForm.GetVersionString();

            List<Tuple<string, string>> values = new List<Tuple<string, string>>()
            {
               Tuple.Create("",  ""),
               Tuple.Create("ApplicationVersion", v),
               Tuple.Create("Contact",  Constants.PUBLISHER),
               Tuple.Create("DisplayName",  Constants.PERUN2),
               Tuple.Create("DisplayIcon",  Paths.GetInstance().GetGuiPath()),
               Tuple.Create("DisplayVersion",  v),
               Tuple.Create("HelpLink",  Constants.WEBSITE_DOCS),
               Tuple.Create("InstallDate", DateTime.Now.ToString("yyyyMMdd")),
               Tuple.Create("InstallLocation", Paths.GetInstance().GetInstallationPath().Quoted()),
               Tuple.Create("Publisher",  Constants.PUBLISHER),
               Tuple.Create("UninstallGuid",  Constants.REGISTRY_UNINSTALL_GUID),
               Tuple.Create("UninstallRegKeyPath", Paths.GetInstance().GetUninstallPath().Quoted()),
               Tuple.Create("UninstallString", Paths.GetInstance().GetUninstallPath().Quoted()),
               Tuple.Create("URLInfoAbout", Constants.WEBSITE),
               Tuple.Create("URLUpdateInfo", Constants.WEBSITE)
            };

            RegistryAction.AddKeyToLocalMachine(UninstallRegistry, values);
            return true;
        }

        public override void Undo()
        {
            RegistryAction.RemoveKeyFromLocalMachine(UninstallRegistry);
            RegistryAction.RemoveKeyFromLocalMachine(UninstallRegistry32on64);
        }
    }
}
