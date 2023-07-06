## How to build for Windows OS?

To build this project, you need WinForms installed. Follow steps below. These with (*) are optional.

1. You have to ask yourself one question. Should this program download its actualizations from the official Perun2 website *https://perun2.org*?
If you build this project on your own, then the answer is probably *no*. So, enter file *Gui/Perun2Gui/Constants.cs* and change the boolean constant *ACTUALIZATIONS_ENABLED*.
2. Compile executable file *perun2.exe* for Windows [from here](https://github.com/wojfil/perun2).
3. (*) Sign this file wth your certificate.
4. Compile three programs. Source is in directories: *Gui*, *Manager*, *Uninstaller*.
5. (*) Sign them with your certificate as well.
6. Copy all four programs to *Installer/Perun2Installer/Resources*.
  Their names should be: *perun2.exe*, *Perun2Gui.exe*, *Perun2Manager.exe*, *uninstall.exe*.
7. Compile the installer (directory *Installer*).
8. (*) Finally, sign this executable wth your certificate.
