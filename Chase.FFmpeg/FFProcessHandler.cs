using Chase.FFmpeg.Downloader;
using System.Diagnostics;

namespace Chase.FFmpeg;

public static class FFProcessHandler
{
    /// <summary>
    /// Executes a command to the ffmpeg executable
    /// </summary>
    /// <param name="arguments">The ffmpeg arguments</param>
    /// <param name="data_handler">Executes when ffmpeg outputs a line to the console</param>
    /// <param name="exited">Executes when ffmpeg process stops running</param>
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
                RedirectStandardOutput = true,
            },
            EnableRaisingEvents = true,
        };

        if (data_handler != null)
        {
            process.ErrorDataReceived += data_handler;
            process.OutputDataReceived += data_handler;
        }
        if (exited != null)
            process.Exited += exited;
        process.Start();
        process.BeginErrorReadLine();
        process.BeginOutputReadLine();
        process.WaitForExit();
        process.Close();
    }

    /// <summary>
    /// Executes a command to the ffprobe executable
    /// </summary>
    /// <param name="arguments">The ffprobe arguments</param>
    /// <param name="data_handler">Executes when ffprobe outputs a line to the console</param>
    /// <param name="exited">Executes when ffprobe process stops running</param>
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
        process.BeginOutputReadLine();
        process.WaitForExit();
        process.Close();
    }
}
