/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Newtonsoft.Json;
using System.Diagnostics;

namespace Chase.FFmpeg.Capture.Data;

//public record WindowedProcess(string path, string title, Process process);

public struct WindowedProcess
{
    public string Path { get; set; }
    public string Title { get; set; }

    [JsonIgnore]
    public Process Process { get; set; }

    public WindowedProcess(Process process)
    {
        Process = process;
        Path = process.MainModule?.FileName ?? "";
        Title = process.MainWindowTitle ?? "";
    }
}