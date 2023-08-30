// LFInteractive LLC. - All Rights Reserved
using System.Diagnostics;

namespace Chase.FFmpeg.Capture.Utilities;

public static class WindowHandler
{
    public static string[] GetWindowTitles()
    {
        List<string> result = new();
        Process[] processes = Process.GetProcesses();
        foreach (Process process in processes)
        {
            if (!string.IsNullOrEmpty(process.MainWindowTitle))
                result.Add(process.MainWindowTitle);
        }
        return result.ToArray();
    }
}