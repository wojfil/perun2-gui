﻿/*
    This file is part of Perun2.
    Perun2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    Perun2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with Perun2. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Perun2Uninstaller;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace Perun2Uninstaller
{
    public partial class MainForm : Form
    {
        private void Uninstall()
        {
            if (Actions.Run())
            {
                FinallyRemoveItself();
                Application.Exit();
            }
        }

        private void FinallyRemoveItself()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe",
                    String.Format("/k {0} & {1} & {2}",
                        "timeout /T 2 /NOBREAK >NUL",
                        "rmdir /s /q \"" + Application.StartupPath + "\"",
                        "exit"
                    )
                );
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                Process.Start(psi);
            }
            catch (Exception)
            {
                Popup.Ok("Uninstallation is almost completed. The uninstall.exe file failed to delete itself. You have to do it manually in order to finish the process.");
            }
        }
    }
}
