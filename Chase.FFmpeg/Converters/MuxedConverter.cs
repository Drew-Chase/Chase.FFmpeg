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
    public MuxedConverter ChangeVideoCodec(string codec)
    {
        _postInputBuilder.Append($" -c:v {codec} ");
        return this;
    }
    public MuxedConverter ChangeAudioCodec(string codec)
    {
        _postInputBuilder.Append($" -c:a {codec} ");
        return this;
    }
    public MuxedConverter ChangeVideoBitrate(string bitrate)
    {
        _postInputBuilder.Append($" -b:v {bitrate}");
        return this;
    }
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
    /// 
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    public MuxedConverter ChangeHardwareAccelerationDevice(string device = "auto")
    {
        _preInputBuilder.Append($" -hwaccel {device} ");
        return this;
    }

    public MuxedConverter OverwriteOriginal()
    {
        _preInputBuilder.Append(" -y ");
        return this;
    }

    public void Convert(string output_file, DataReceivedEventHandler? data_handler, EventHandler? exited) => FFProcessHandler.ExecuteFFmpeg(Build(output_file), data_handler, exited);

    public string Build(string output_file) => $"{_preInputBuilder.ToString().Trim()} -i \"{Info.Path}\" {_postInputBuilder} -vf \"{_videoFormat.ToString().Trim()}\" \"{output_file.Trim()}\"".Replace("  ", " ").Trim();

    private MuxedConverter(MediaInfo info)
    {
        _preInputBuilder = new StringBuilder();
        _postInputBuilder = new StringBuilder();
        _videoFormat = new StringBuilder();
        Info = info;
    }
}
// MuxedConverter.SetMedia().ChangeFramerate(15).ChangeResolution(800,600).ChangeCodec("h264_nvenc")