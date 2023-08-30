/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

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