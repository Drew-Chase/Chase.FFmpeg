/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Chase.FFmpeg.Exceptions;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Chase.FFmpeg.Info;

/// <summary>
/// Class for compiling and handling general media information
/// </summary>
public sealed class FFMediaInfo
{
    /// <summary>
    /// The duration as a timespan
    /// </summary>
    public TimeSpan Duration { get; private set; }

    /// <summary>
    /// The name of the file
    /// </summary>
    public string Filename { get; private set; }

    /// <summary>
    /// The path to the file
    /// </summary>
    public string Path { get; private set; }

    /// <summary>
    /// The size of the file in bytes
    /// </summary>
    public ulong Size { get; private set; }

    /// <summary>
    /// The overall birate
    /// </summary>
    public ulong BitRate { get; private set; }

    /// <summary>
    /// A array of all streams
    /// </summary>
    public FFMediaStream[]? Streams { get; private set; }

    /// <summary>
    /// General media information
    /// </summary>
    /// <param name="file"></param>
    /// <param name="useQuickMath">
    /// If the percentage should be calculated by the duration * framerate or by getting the exact
    /// frame count from ffprobe. This can have major performance impacts!
    /// </param>
    /// <exception cref="NotMediaFileException"/>
    public FFMediaInfo(string file, bool useQuickMath = true)
    {
        FileInfo info = new(file);
        Size = (ulong)info.Length;
        Filename = info.Name;
        Path = file;
        StringBuilder jsonBuilder = new();
        FFProcessHandler.ExecuteFFprobe($"-loglevel 0 -print_format json -show_format -show_streams {(useQuickMath ? "" : "-count_frames")} \"{file}\"", (s, e) =>
        {
            string? content = e.Data;
            if (!string.IsNullOrWhiteSpace(content))
            {
                jsonBuilder.AppendLine(content);
            }
        }, null);

        JObject json = JObject.Parse(jsonBuilder.ToString());

        if (json.Count == 0)
            throw new NotMediaFileException(file);

        JObject? formats = GetItem<JObject>("format", json);
        if (formats != null)
        {
            BitRate = GetItem<ulong>("bit_rate", formats);
            Duration = TimeSpan.FromSeconds(GetItem<double>("duration", formats));
        }

        JArray? streams = GetItem<JArray>("streams", json);
        if (streams != null)
        {
            List<FFMediaStream> temp_streams = new();
            foreach (JObject stream in streams)
            {
                int index = GetItem<int>("index", stream);
                string codecName = GetItem<string>("codec_name", stream) ?? "";
                string codecLongName = GetItem<string>("codec_long_name", stream) ?? "";
                string codecType = GetItem<string>("codec_type", stream) ?? "";
                string profile = GetItem<string>("profile", stream) ?? "";
                int? width = GetItem<int>("width", stream);
                int? height = GetItem<int>("height", stream);
                string aspectRatio = GetItem<string>("display_aspect_ratio", stream) ?? "";
                string pixelFormat = GetItem<string>("pix_fmt", stream) ?? "";
                int level = GetItem<int>("level", stream);
                string colorRange = GetItem<string>("color_range", stream) ?? "";
                double? frameRate = 0;

                string? sfps = GetItem<string>("avg_frame_rate", stream);
                if (sfps != null && sfps != "0/0")
                {
                    int[] parts = Array.ConvertAll(sfps.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries), i => Convert.ToInt32(i));
                    if (parts.Length == 2 && parts[0] != 0 && parts[1] != 0)
                    {
                        frameRate = (double)parts[0] / parts[1];
                    }
                }

                long? frames = 0;
                long? bPS = 0;
                TimeSpan? duration = TimeSpan.Zero;
                JObject? tags = GetItem<JObject>("tags", stream);
                if (tags != null)
                {
                    frames = GetItem<long>("NUMBER_OF_FRAMES", tags);
                    bPS = GetItem<long>("bps", tags);
                    duration = GetItem<TimeSpan>("DURATION", tags);
                }

                if (duration == TimeSpan.Zero)
                {
                    duration = Duration;
                }

                if (frames == 0 && frameRate != 0 && duration != null)
                {
                    frames = (long)(frameRate * duration.Value.TotalSeconds);
                }

                temp_streams.Add(new(index, codecName, codecLongName, codecType, profile, width, height, aspectRatio, pixelFormat, level, colorRange, frameRate, frames, bPS, duration));
            }
            Streams = temp_streams.ToArray();
        }
    }

    private T? GetItem<T>(string name, JObject json)
    {
        if (json[name] != null)
        {
            try
            {
                return json[name].ToObject<T>();
            }
            catch
            {
            }
        }
        return default;
    }
}