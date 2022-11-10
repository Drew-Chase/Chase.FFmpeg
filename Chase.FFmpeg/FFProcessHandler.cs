using Chase.FFmpeg.Downloader;
using Chase.FFmpeg.Events;
using Chase.FFmpeg.Info;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Chase.FFmpeg;

public static class FFProcessHandler
{


    /// <summary>
    /// Executes a command to the ffmpeg executable
    /// </summary>
    /// <param name="arguments">The ffmpeg arguments</param>
    /// <param name="data_handler">Executes when ffmpeg outputs a line to the console</param>
    /// <param name="exited">Executes when ffmpeg process stops running</param>
    public static void ExecuteFFmpeg(string arguments, MediaInfo? info = null, DataReceivedEventHandler? data_handler = null, EventHandler? exited = null, EventHandler<FFProcessUpdateEventArgs>? updated = null)
    {
        float Percentage = 0f;
        uint FramesProcessed = 0;
        float Speed = 0f;
        float AverageBitrate = 0f;
        Process process = new()
        {
            StartInfo = new()
            {
                FileName = FFmpegDownloader.Instance.FFmpegExecutable,
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
        if (exited != null)
            process.Exited += exited;


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
                                string? t = matchObject.ToString().Trim().Replace(" ", "");
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

                        Percentage = ((float)FramesProcessed / info.VideoStream.Frames) * 100;
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
