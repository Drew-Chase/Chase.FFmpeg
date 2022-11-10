using Chase.FFmpeg.Events;
using Chase.FFmpeg.Info;
using System.Diagnostics;
using System.Text;

namespace Chase.FFmpeg.Converters;

public sealed class MuxedConverter
{
    public MediaInfo Info { get; private set; }
    private StringBuilder _postInputBuilder, _preInputBuilder, _videoFormat;
    public static MuxedConverter SetMedia(MediaInfo info)
    {
        return new(info);
    }

    /// <summary>
    /// Changes the video codec
    /// </summary>
    /// <param name="codec"></param>
    /// <returns></returns>
    public MuxedConverter ChangeVideoCodec(string codec)
    {
        _postInputBuilder.Append($" -c:v {codec} ");
        return this;
    }

    /// <summary>
    /// Changes the audio codec
    /// </summary>
    /// <param name="codec"></param>
    /// <returns></returns>
    public MuxedConverter ChangeAudioCodec(string codec)
    {
        _postInputBuilder.Append($" -c:a {codec} ");
        return this;
    }
    /// <summary>
    /// Changes the video bitrate
    /// </summary>
    /// <param name="bitrate"></param>
    /// <returns></returns>
    public MuxedConverter ChangeVideoBitrate(string bitrate)
    {
        _postInputBuilder.Append($" -b:v {bitrate}");
        return this;
    }

    /// <summary>
    /// Changes the audio bitrate
    /// </summary>
    /// <param name="bitrate"></param>
    /// <returns></returns>
    public MuxedConverter ChangeAudioBitrate(string bitrate)
    {
        _postInputBuilder.Append($" -b:a {bitrate} ");
        return this;
    }

    /// <summary>
    /// Changes the width and height of the video stream<br />
    /// This is <b>NOT</b> compatable with <seealso cref="ChangeWidth(int)"/> or <seealso cref="ChangeHeight(int)"/>
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public MuxedConverter ChangeResolution(int width, int height)
    {
        _videoFormat.Append($" scale={width}:{height} ");
        return this;
    }
    /// <summary>
    /// Changes the width of the video while maintaining aspect ratio<br />
    /// 
    /// This is <b>NOT</b> compatable with <seealso cref="ChangeHeight(int)"/> or <seealso cref="ChangeResolution(int, int)"/>
    /// </summary>
    /// <param name="width"></param>
    /// <returns></returns>
    public MuxedConverter ChangeWidth(int width)
    {
        _videoFormat.Append($" scale={width}:-1 ");
        return this;
    }
    /// <summary>
    /// Changes the height of the video while maintaining aspect ratio<br />
    /// This is <b>NOT</b> compatable with <seealso cref="ChangeResolution(int, int)"/> or <seealso cref="ChangeWidth(int)"/>
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public MuxedConverter ChangeHeight(int height)
    {
        _videoFormat.Append($" scale=-1:{height} ");
        return this;
    }

    /// <summary>
    /// Check <seealso cref="SupportedHardwareAccelerationMethods"/> for list of supported methods.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public MuxedConverter ChangeHardwareAccelerationMethod(string method = "auto")
    {
        _preInputBuilder.Append($" -hwaccel {method} ");
        return this;
    }

    /// <summary>
    /// Overwrites oringal file
    /// </summary>
    /// <returns></returns>
    public MuxedConverter OverwriteOriginal()
    {
        _preInputBuilder.Append(" -y ");
        return this;
    }

    /// <summary>
    /// Changes the pixel format of the video
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public MuxedConverter ChangePixelFormat(string format)
    {
        _postInputBuilder.Append($" -pix_fmt {format} ");
        return this;
    }

    public MuxedConverter ChangeStartPosition(string position)
    {
        _preInputBuilder.Append($" -ss {position}");
        return this;
    }
    public MuxedConverter ChangeVideoDuration(string position)
    {
        _postInputBuilder.Append($" -t {position}");
        return this;
    }

    /// <summary>
    /// Adds an argument after the input option
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public MuxedConverter AddCustomPostInputOption(string option)
    {
        _postInputBuilder.Append($" {option} ");
        return this;
    }

    /// <summary>
    /// Adds an argument before the input option
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public MuxedConverter AddCustomPreInputOption(string option)
    {
        _preInputBuilder.Append($" {option} ");
        return this;
    }


    /// <summary>
    /// Adds a video format option
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public MuxedConverter AddCustomVideoFormatOption(string option)
    {
        _videoFormat.Append($" {option} ");
        return this;
    }
    /// <summary>
    /// Builds the ffmpeg argument and starts the convertion proess
    /// </summary>
    /// <param name="output_file"></param>
    /// <param name="data_handler"></param>
    /// <param name="exited"></param>
    /// <param name="updated"></param>
    public void Convert(string output_file, DataReceivedEventHandler? data_handler, EventHandler? exited, EventHandler<FFProcessUpdateEventArgs>? updated) => FFProcessHandler.ExecuteFFmpeg(Build(output_file), Info, data_handler, exited, updated);

    /// <summary>
    /// Builds and returns the ffmpeg argument
    /// </summary>
    /// <param name="output_file"></param>
    /// <returns></returns>
    public string Build(string output_file) => $"{_preInputBuilder.ToString().Trim()} -i \"{Info.Path}\" {_postInputBuilder} {(!string.IsNullOrWhiteSpace(_videoFormat.ToString()) ? $"-vf \"{_videoFormat.ToString().Trim()}\"" : "")} \"{output_file.Trim()}\"".Replace("  ", " ").Trim();

    private MuxedConverter(MediaInfo info)
    {
        _preInputBuilder = new StringBuilder();
        _postInputBuilder = new StringBuilder();
        _videoFormat = new StringBuilder();

        Info = info;
    }
}
// MuxedConverter.SetMedia().ChangeFramerate(15).ChangeResolution(800,600).ChangeCodec("h264_nvenc")