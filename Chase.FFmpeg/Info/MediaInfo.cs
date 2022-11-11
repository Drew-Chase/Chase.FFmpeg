using Newtonsoft.Json.Linq;
using System.Text;

namespace Chase.FFmpeg.Info;

/// <summary>
/// Class for compiling and handling general media information
/// </summary>
public sealed class MediaInfo
{
    /// <summary>
    /// The size of the file in bytes
    /// </summary>
    public ulong Size { get; private set; }
    /// <summary>
    /// The file size in human readable notation
    /// </summary>
    public string SizeENG { get; private set; }
    /// <summary>
    /// The duration as a timespan
    /// </summary>
    public TimeSpan DurationENG { get; private set; }
    /// <summary>
    /// The duration in seconds
    /// </summary>
    public double Duration { get; private set; }
    /// <summary>
    /// The name of the file
    /// </summary>
    public string Filename { get; private set; }
    /// <summary>
    /// The path to the file
    /// </summary>
    public string Path { get; private set; }
    /// <summary>
    /// The video stream information
    /// </summary>
    public VideoStreamInfo VideoStream { get; private set; }
    /// <summary>
    /// The audio stream information
    /// </summary>
    public AudioStreamInfo AudioStream { get; private set; }

    /// <summary>
    /// General media information
    /// </summary>
    /// <param name="file"></param>
    /// <param name="useQuickMath">If the percentage should be calculated by the duration * framerate or by getting the exact frame count from ffprobe</param>
    public MediaInfo(string file, bool useQuickMath = true)
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

        // Media Format
        ulong bit_rate = 0;
        JObject? format = (JObject)json["format"];
        if (format != null)
        {
            if (format["bit_rate"] != null)
            {
                bit_rate = Convert.ToUInt64((string)format["bit_rate"]);
            }
            if (format["duration"] != null && float.TryParse((string)format["duration"], out float _duration))
            {
                Duration = _duration;
                DurationENG = TimeSpan.FromSeconds(_duration);
            }
        }

        JArray streams = (JArray)json["streams"];
        foreach (JObject stream in streams)
        {
            string codec_type = "N/A";
            if (stream["codec_type"] != null)
            {
                codec_type = (string)stream["codec_type"];
            }

            if (codec_type.ToLower().Equals("video"))
            {
                double framerate = 0d;
                uint width = 0, height = 0, frames = 0;
                string codec = "", pixel_format = "", aspect_ratio = "";
                if (stream["width"] != null)
                {
                    width = Convert.ToUInt32((string)stream["width"]);
                }
                if (stream["height"] != null)
                {
                    height = Convert.ToUInt32((string)stream["height"]);
                }
                if (stream["codec_name"] != null)
                {
                    codec = (string)stream["codec_name"];
                }

                if (stream["pix_fmt"] != null)
                {
                    pixel_format = (string)stream["pix_fmt"];
                }
                if (stream["display_aspect_ratio"] != null)
                {
                    aspect_ratio = (string)stream["display_aspect_ratio"];
                }
                if (stream["tags"] != null)
                {
                    if (stream["tags"]["NUMBER_OF_FRAMES"] != null)
                    {
                        frames = Convert.ToUInt32((string)stream["tags"]["NUMBER_OF_FRAMES"]);
                    }
                }
                if (stream["avg_frame_rate"] != null)
                {
                    string partial = (string)stream["avg_frame_rate"];
                    uint[] parts = Array.ConvertAll(partial.Split("/"), i => Convert.ToUInt32(i));
                    framerate = (double)parts[0] / parts[1];
                }

                if (!useQuickMath && stream["nb_read_frames"] != null)
                {
                    frames = Convert.ToUInt32(stream["nb_read_frames"]);
                }
                else
                {
                    frames = (uint)(framerate * Duration);
                }

                VideoStream = new(frames, bit_rate, width, height, pixel_format, framerate, aspect_ratio, codec);
            }
            else if (codec_type.ToLower().Equals("audio"))
            {
                uint size = 0, sample_rate = 0, channels = 0;
                string codec = "", sample_format = "", channel_layout = "";

                if (stream["codec_name"] != null)
                {
                    codec = (string)stream["codec_name"];
                }
                if (stream["sample_fmt"] != null)
                {
                    sample_format = (string)stream["sample_fmt"];
                }
                if (stream["sample_rate"] != null)
                {
                    sample_rate = Convert.ToUInt32((string)stream["sample_rate"]);
                }
                if (stream["channel_layout"] != null)
                {
                    channel_layout = (string)stream["channel_layout"];
                }
                if (stream["extradata_size"] != null)
                {
                    size = Convert.ToUInt32((int)stream["extradata_size"]);
                }
                if (stream["channels"] != null)
                {
                    channels = Convert.ToUInt32((string)stream["channels"]);
                }



                AudioStream = new(codec, sample_rate, sample_format, channel_layout, size, channels);
            }
        }

        SizeENG = CLMath.CLFileMath.AdjustedFileSize(Size);
    }
}
