/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Chase.FFmpeg.Downloader;
using Chase.FFmpeg.Events;
using Chase.FFmpeg.Info;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Chase.FFmpeg;

/// <summary>
/// Adds functions to run ffmpeg/ffprobe
/// </summary>
public static class FFProcessHandler
{
    /// <summary>
    /// Executes a command to the ffmpeg executable
    /// </summary>
    /// <param name="arguments">The ffmpeg arguments</param>
    /// <param name="info"></param>
    /// <param name="data_handler">Executes when ffmpeg outputs a line to the console</param>
    /// <param name="updated"></param>
    /// <param name="auto_start">If the program should automatically start and wait for exit!</param>
    public static Process ExecuteFFmpeg(string arguments, FFMediaInfo? info = null, DataReceivedEventHandler? data_handler = null, EventHandler<FFProcessUpdateEventArgs>? updated = null, bool auto_start = true)
    {
        float Percentage = 0f;
        uint FramesProcessed = 0;
        float Speed = 0f;
        float AverageBitrate = 0f;
        Process process = new()
        {
            StartInfo = new()
            {
                FileName = FFmpegDownloader.Instance.LoadedInstallation.FFmpeg,
                Arguments = arguments,
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

        if (info != null)
        {
            process.ErrorDataReceived += (s, e) =>
            {
                string? content = e.Data;
                if (!string.IsNullOrWhiteSpace(content))
                {
                    if (content.StartsWith("frame="))
                    {
                        MatchCollection regexMatches = Regex.Matches(content, "((?!\\s).)*[A-z]=((?!(\\s[A-z])).)*");
                        foreach (object matchObject in regexMatches)
                        {
                            try
                            {
                                string t = matchObject?.ToString()?.Trim().Replace(" ", "") ?? "";
                                string[] parts = t.Split('=');
                                if (parts.Length == 2)
                                {
                                    switch (parts[0])
                                    {
                                        case "frame":
                                            FramesProcessed = Convert.ToUInt32(parts[1]);
                                            break;

                                        case "fps":
                                            break;

                                        case "size":
                                            break;

                                        case "time":
                                            break;

                                        case "bitrate":
                                            string d = parts[1].Replace("kbits/s", "");
                                            AverageBitrate = Convert.ToSingle(parts[1].Replace("kbits/s", ""));
                                            break;

                                        case "speed":
                                            Speed = Convert.ToSingle(parts[1].Trim('x'));
                                            break;

                                        default: break;
                                    }
                                }
                            }
                            catch { }
                        }
                        Percentage = (float)(FramesProcessed / (float)info.Streams.First(i => i.CodecType.Equals("video", StringComparison.OrdinalIgnoreCase)).Frames);
                        updated?.Invoke(null, new()
                        {
                            Speed = Speed,
                            AverageBitrate = AverageBitrate,
                            FramesProcessed = FramesProcessed,
                            Percentage = Percentage,
                        });
                    }
                }
            };
        }
        if (auto_start)
        {
            process.Start();
            try
            {
                AppDomain.CurrentDomain.ProcessExit += (s, e) =>
                {
                    try
                    {
                        if (process != null && !process.HasExited)
                            process?.Kill();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR: {ex.Message}");
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.Close();
        }

        return process;
    }

    /// <summary>
    /// Executes a command to the ffprobe executable
    /// </summary>
    /// <param name="arguments">The ffprobe arguments</param>
    /// <param name="data_handler">Executes when ffprobe outputs a line to the console</param>
    /// <param name="exited">Executes when ffprobe process stops running</param>
    public static void ExecuteFFprobe(string arguments, DataReceivedEventHandler? data_handler, EventHandler? exited)
    {
        try
        {
            Process process = new()
            {
                StartInfo = new()
                {
                    FileName = FFmpegDownloader.Instance.LoadedInstallation.FFProbe,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
                EnableRaisingEvents = true,
            };

            if (data_handler != null)
            {
                process.OutputDataReceived += data_handler;
                process.ErrorDataReceived += data_handler;
            }
            if (exited != null)
                process.Exited += exited;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}