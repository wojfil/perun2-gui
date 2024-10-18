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
7. Prepare external dependencies. As of now, there is only one dependency - FFmpeg. 
You should copy 8 compiled DLL libraries to the Resources (the same place as executables in previous steps).
Their names: avcodec-61.dll, avdevice-61.dll, avfilter-10.dll, avformat-61.dll, avutil-59.dll, 
postproc-58.dll, swresample-5.dll, swscale-8.dll. How to get them is described [here](https://github.com/wojfil/perun2/tree/master/external).
8. Compile the installer (directory *Installer*).
9. (*) Finally, sign this executable wth your certificate.
