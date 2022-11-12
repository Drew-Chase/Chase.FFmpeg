namespace Chase.FFmpeg.Info;

/// <summary>
/// Stores information about the video stream
/// </summary>
public struct FFVideoStreamInfo
{
    internal FFVideoStreamInfo(ulong frames, ulong bit_rate, uint width, uint height, string pixel_format, double framerate, string aspect_ratio, string codec)
    {
        Frames = frames;
        BitRate = bit_rate;
        Width = width;
        Height = height;
        PixelFormat = pixel_format;
        Framerate = framerate;
        AspectRatio = aspect_ratio;
        Codec = codec;
        BitRateENG = CLMath.CLFileMath.AdjustedFileSize(BitRate) + "s";
    }

    /// <summary>
    /// The aspect ratio of the video stream
    /// </summary>
    public string AspectRatio { get; private set; }

    /// <summary>
    /// The bit rate of the video in bytes
    /// </summary>
    public ulong BitRate { get; private set; }

    /// <summary>
    /// The bit rate of the video in human readable notation
    /// </summary>
    public string BitRateENG { get; private set; }

    /// <summary>
    /// The name of the codec used to encode the video stream
    /// </summary>
    public string Codec { get; private set; }

    /// <summary>
    /// The average framerate of the video stream
    /// </summary>
    public double Framerate { get; private set; }

    /// <summary>
    /// The number of frames in a video
    /// </summary>
    public ulong Frames { get; private set; }
    /// <summary>
    /// The height of the video stream
    /// </summary>
    public uint Height { get; private set; }

    /// <summary>
    /// The pixel format of the video stream
    /// </summary>
    public string PixelFormat { get; private set; }

    /// <summary>
    /// The width of the video stream
    /// </summary>
    public uint Width { get; private set; }
}
