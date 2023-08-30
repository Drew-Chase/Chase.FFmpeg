/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Chase.FFmpeg.Events;
using Chase.FFmpeg.Info;
using System.Diagnostics;
using System.Text;

namespace Chase.FFmpeg.Converters;

/// <summary>
/// Stream types for
/// </summary>
public enum StreamType
{
    Video,
    Audio,
    Subtitle
}

/// <summary>
/// For converting video and audio streams
/// </summary>
public sealed class FFMuxedConverter
{
    private readonly StringBuilder _postInputBuilder, _preInputBuilder, _videoFormat, _inputsBuilder;

    /// <summary>
    /// The input file
    /// </summary>
    public FFMediaInfo Info { get; private set; }

    private FFMuxedConverter(FFMediaInfo info)
    {
        _preInputBuilder = new StringBuilder();
        _postInputBuilder = new StringBuilder();
        _videoFormat = new StringBuilder();
        _inputsBuilder = new StringBuilder();

        _inputsBuilder.AppendLine($" -i \"{info.Path}\" ");

        Info = info;
    }

    /// <summary>
    /// Sets the input file
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static FFMuxedConverter SetMedia(FFMediaInfo info)
    {
        return new(info);
    }

    /// <summary>
    /// Adds an argument after the input option <br/><br/> FFMpeg argument: <b>{option}</b>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public FFMuxedConverter AddCustomPostInputOption(string option)
    {
        _postInputBuilder.Append($" {option} ");
        return this;
    }

    /// <summary>
    /// Adds an argument before the input option <br/><br/> FFMpeg argument: <b>{option}</b>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public FFMuxedConverter AddCustomPreInputOption(string option)
    {
        _preInputBuilder.Append($" {option} ");
        return this;
    }

    /// <summary>
    /// Adds a video format option <br/><br/> FFMpeg argument: <b>{option}</b>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public FFMuxedConverter AddCustomVideoFormatOption(string option)
    {
        _videoFormat.Append($" {option} ");
        return this;
    }

    /// <summary>
    /// Builds and returns the ffmpeg argument
    /// </summary>
    /// <param name="output_file"></param>
    /// <returns></returns>
    public string Build(string output_file) => $"{_preInputBuilder.ToString().Trim()} {_inputsBuilder.ToString().Trim()} {_postInputBuilder} {(!string.IsNullOrWhiteSpace(_videoFormat.ToString()) ? $"-vf \"{_videoFormat.ToString().Trim()}\"" : "")} \"{output_file.Trim()}\"".Replace("  ", " ").Trim();

    /// <summary>
    /// Changes the audio bitrate <br/><br/> FFMpeg argument: <b>-b:a {bitrate}</b>
    /// </summary>
    /// <param name="bitrate"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeAudioBitrate(string bitrate)
    {
        _postInputBuilder.Append($" -b:a {bitrate} ");
        return this;
    }

    /// <summary>
    /// Changes the audio codec <br/><br/> FFMpeg argument: <b>-c:a {codec}</b>
    /// </summary>
    /// <param name="codec"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeAudioCodec(string codec)
    {
        _postInputBuilder.Append($" -c:a {codec} ");
        return this;
    }

    /// <summary>
    /// Check <seealso cref="FFSupportedHardwareAccelerationMethods"/> for list of supported
    /// methods. <br/><br/> FFMpeg argument: <b>-hwaccel {method}</b>
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeHardwareAccelerationMethod(string method = "auto")
    {
        _preInputBuilder.Append($" -hwaccel {method} ");
        return this;
    }

    /// <summary>
    /// Changes the height of the video while maintaining aspect ratio <br/> This is <b>NOT</b>
    /// compatable with <seealso cref="ChangeResolution(int, int)"/> or <seealso
    /// cref="ChangeWidth(int)"/><br/><br/> FFMpeg argument: <b>scale=-1:{height}</b>
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeHeight(int height)
    {
        _videoFormat.Append($" scale=-1:{height} ");
        return this;
    }

    /// <summary>
    /// Changes the pixel format of the video <br/><br/> FFMpeg argument: <b>-pix_fmt {format}</b>
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangePixelFormat(string format)
    {
        _postInputBuilder.Append($" -pix_fmt {format} ");
        return this;
    }

    /// <summary>
    /// Changes the width and height of the video stream <br/> This is <b>NOT</b> compatable with
    /// <seealso cref="ChangeWidth(int)"/> or <seealso cref="ChangeHeight(int)"/><br/><br/> FFMpeg
    /// argument: <b>-vf "scale={width}:{height}"</b>
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeResolution(int width, int height)
    {
        _videoFormat.Append($" scale={width}:{height} ");
        return this;
    }

    /// <summary>
    /// Changes the start position of the converted file <br/><br/> FFMpeg argument: <b>-ss {position}</b>
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeStartPosition(TimeSpan position)
    {
        _preInputBuilder.Append($" -ss {position} ");
        return this;
    }

    /// <summary>
    /// Changes the video bitrate <br/><br/> FFMpeg argument: <b>-b:v {bitrate}</b>
    /// </summary>
    /// <param name="bitrate"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeVideoBitrate(string bitrate)
    {
        _postInputBuilder.Append($" -b:v {bitrate} ");
        return this;
    }

    /// <summary>
    /// Changes the video codec <br/><br/> FFMpeg argument: <b>-c:v {codec}</b>
    /// </summary>
    /// <param name="codec"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeVideoCodec(string codec)
    {
        _postInputBuilder.Append($" -c:v {codec} ");
        return this;
    }

    /// <summary>
    /// Changes the the duration of the video <br/><br/> FFMpeg argument: <b>-t {position}</b>
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeVideoDuration(TimeSpan position)
    {
        _postInputBuilder.Append($" -t {position} ");
        return this;
    }

    /// <summary>
    /// Changes the width of the video while maintaining aspect ratio <br/>
    ///
    /// This is <b>NOT</b> compatable with <seealso cref="ChangeHeight(int)"/> or <seealso
    /// cref="ChangeResolution(int, int)"/><br/><br/> FFMpeg argument: <b>-vf "scale={width}:-1"</b>
    /// </summary>
    /// <param name="width"></param>
    /// <returns></returns>
    public FFMuxedConverter ChangeWidth(int width)
    {
        _videoFormat.Append($" scale={width}:-1 ");
        return this;
    }

    /// <summary>
    /// Builds the ffmpeg argument and starts the convertion proess
    /// </summary>
    /// <param name="output_file"></param>
    /// <param name="data_handler"></param>
    /// <param name="updated"></param>
    /// <param name="auto_start">If the program should automatically start and wait for exit!</param>
    public Process Convert(string output_file, DataReceivedEventHandler? data_handler, EventHandler<FFProcessUpdateEventArgs>? updated, bool auto_start = true) => FFProcessHandler.ExecuteFFmpeg(Build(output_file), Info, data_handler, updated, auto_start);

    /// <summary>
    /// Merges video and audio streams to one file <br/><br/> FFMpeg argument: <b>-i "{video}" -i "{audio}"</b>
    /// </summary>
    /// <param name="video"></param>
    /// <param name="audio"></param>
    /// <returns></returns>
    public FFMuxedConverter MuxStreams(string video, string audio)
    {
        _inputsBuilder.Clear();
        _inputsBuilder.AppendLine($" -i \"{video}\" -i \"{audio}\" ");
        return this;
    }

    /// <summary>
    /// Overwrites oringal file. <br/><br/> FFMpeg argument: <b>-y</b>
    /// </summary>
    /// <returns></returns>
    public FFMuxedConverter OverwriteOriginal()
    {
        _preInputBuilder.Append(" -y ");
        return this;
    }

    /// <summary>
    /// Selects the stream to output. <br/><br/> FFMpeg argument: <b>-map {type}:{index}"</b>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public FFMuxedConverter SelectStream(StreamType type, int index)
    {
        _postInputBuilder.Append(" -map ");
        switch (type)
        {
            case StreamType.Video:
                _postInputBuilder.Append("v:");
                break;

            case StreamType.Audio:
                _postInputBuilder.Append("a:");
                break;

            case StreamType.Subtitle:
                _postInputBuilder.Append("s:");
                break;
        }
        _postInputBuilder.Append($"{index} ");
        return this;
    }
}