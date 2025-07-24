using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace Perun2Gui
{
    public partial class MainForm : Form
    {
        // these two strings are members of this class
        // and should still be (some stability reasons)
        private string NextLogLines = "";
        private string FinalLogLine = "";


        private void Run(ExecutionMode mode)
        {
            if (logBackgroundWorker.IsBusy)
            {
                return;
            }

            string exePath = Paths.GetInstance().GetExePath();

            if (!File.Exists(exePath))
            {
                logBox.AppendText("Error! The language file 'perun2.exe' is missing. To solve this problem, actualize Perun2 (Top Menu -> Help -> Actualization)."
                    + NEW_LINE + LINE);
                return;
            }

            if (mode == ExecutionMode.Run && !Directory.Exists(state.LocationPathString))
            {
                logBox.AppendText("Error! Current working location does not exist anymore." + NEW_LINE + LINE);
                return;
            }

            /*if (mode == ExecutionMode.Run && IsCurrentFileGlobalScript())
            {
                logBox.AppendText("Error! This is a global script. You can run it only from the dropdown menu of the File Explorer."
                    + NEW_LINE + LINE);
                return;
            }*/

            lock (SyncGate)
            {
                if (Process != null) return;
            }

            if (! state.HasBackup() && state.HasFile() && !File.Exists(state.FilePathString))
            {
                SetBackup(codeBox.Text);
            }

            Process = new Process();

            if (mode == ExecutionMode.Run) {
                Process.StartInfo.WorkingDirectory = state.LocationPathString;
            }

            Process.StartInfo.Arguments = GetRunnerArgs(mode);
            Process.StartInfo.FileName = exePath;
            Process.StartInfo.CreateNoWindow = true;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.OutputDataReceived += OnOutputDataReceived;
            Process.EnableRaisingEvents = true;

            Running = true;
            RefreshRunButton();
            RunStartRefreshForm();

            WaitingLogs.Clear();
            AnyWaitingLogs = false;

            Process.Start();
            Process.BeginOutputReadLine();

            logBackgroundWorker.RunWorkerAsync();
        }

        private void WorkerStart()
        {
            while (Running && !Stopped)
            {
                Thread.Sleep(LOG_SLEEP);
                // instead of printing logs one by one
                // aggregate them and print huge chunks

                lock (SyncGate)
                {
                    if (AnyWaitingLogs)
                    {
                        NextLogLines = WaitingLogs.ToString();
                        WaitingLogs.Clear();
                        AnyWaitingLogs = false;

                        Invoke((MethodInvoker)delegate
                        {
                            logBox.AppendText(NextLogLines);
                        });
                    }
                }
            }
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && sender == Process)
            {
                lock (SyncGate)
                {
                    AnyWaitingLogs = true;
                    byte[] bytes = Encoding.Default.GetBytes(e.Data);
                    string ed = Encoding.UTF8.GetString(bytes);
                    if (LogsRecentlyCleaned)
                    {
                        WaitingLogs.Append(ed);
                        LogsRecentlyCleaned = false;
                    }
                    else
                    {
                        WaitingLogs.Append(NEW_LINE + ed);
                        LogLinesCount++;
                    }
                }
            }
            else
            {
                var d2 = new LogBoxDelegate(new Action(() =>
                {
                    FinishLine();
                }));
                logBox.BeginInvoke(d2);
            }
        }

        private void WorkerFinish()
        {
            lock (SyncGate)
            {
                FinalLogLine = WaitingLogs.ToString() + NEW_LINE + LINE;
                WaitingLogs.Clear();
                AnyWaitingLogs = false;

                Invoke((MethodInvoker)delegate
                {
                    logBox.AppendText(FinalLogLine);
                });
            }
        }

        private void FinishLine()
        {
            Process.Dispose();
            Process = null;
            Running = false;
            Stopped = false;
            RefreshRunButton();
            RunStopRefreshForm();
        }

        private void RunStop()
        {
            if (Running)
            {
                Running = false;
                Stopped = true;
                RefreshRunButton();

                new Thread(() =>
                {
                    if (AttachConsole((uint)Process.Id))
                    {
                        SetConsoleCtrlHandler(null, true);
                        try
                        {
                            if (!GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0))
                            {
                                Running = true;
                                Stopped = false;
                                RefreshRunButton();
                                return;
                            }
                            Process.WaitForExit();
                        }
                        finally
                        {
                            SetConsoleCtrlHandler(null, false);
                            FreeConsole();
                        }

                        return;
                    }
                }).Start();
            }
        }
    }
}
