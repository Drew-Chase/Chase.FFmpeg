using Chase.FFmpeg.Downloader;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Chase.FFmpeg;

internal static class ProcessHandler
{
    public static void ExecuteFFmpeg(string arguments, DataReceivedEventHandler? data_handler, EventHandler? exited)
    {
        Process process = new()
        {
            StartInfo = new()
            {
                FileName = FFmpegDownloader.Instance.FFmpegExecutable,
                Arguments = arguments,
                CreateNoWindow = true,
                RedirectStandardError = true,
            },
            EnableRaisingEvents = true,
        };

        if (data_handler != null)
            process.ErrorDataReceived += data_handler;
        if (exited != null)
            process.Exited += exited;
        process.Start();
        process.BeginErrorReadLine();
        process.WaitForExit();
        process.Close();
    }
    public static void ExecuteFFprobe(string arguments, DataReceivedEventHandler? data_handler, EventHandler? exited)
    {
        Process process = new()
        {
            StartInfo = new()
            {
                FileName = FFmpegDownloader.Instance.FFprobeExecutable,
                Arguments = arguments,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            },
            EnableRaisingEvents = true,
        };

        if (data_handler != null)
            process.OutputDataReceived += data_handler;
        if (exited != null)
            process.Exited += exited;
        process.Start();
        process.BeginErrorReadLine();
        process.WaitForExit();
        process.Close();
    }
}
