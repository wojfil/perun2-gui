## How to build for Windows OS?

To build this project, you only need WinForms installed. Follow steps below. These with (*) are optional.

1. Compile executable file *perun2.exe* for Windows [from here](https://github.com/wojfil/perun2).
2. (*) Sign this file wth your certificate.
3. Compile three programs. Source is in directories: *Gui*, *Manager*, *Uninstaller*.
4. (*) Sign them with your certificate as well.
5. Copy all four programs to *Installer/Perun2Installer/Resources*.
  Their names should be: *perun2.exe*, *Perun2Gui.exe*, *Perun2Manager.exe*, *uninstall.exe*.
6. Compile the installer (directory *Installer*).
7. (*) Finally, sign this executable wth your certificate.
