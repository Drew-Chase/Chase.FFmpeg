/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

public struct FFMediaStream
{
    /// <summary>
    /// Gets the index of the media stream.
    /// </summary>
    public int Index { get; private set; }

    /// <summary>
    /// Gets the name of the codec used for encoding this stream.
    /// </summary>
    public string CodecName { get; private set; }

    /// <summary>
    /// Gets the long name of the codec used for encoding this stream.
    /// </summary>
    public string CodecLongName { get; private set; }

    /// <summary>
    /// Gets the type of codec used for encoding this stream (e.g., audio, video).
    /// </summary>
    public string CodecType { get; private set; }

    /// <summary>
    /// Gets the profile of the codec used for encoding this stream.
    /// </summary>
    public string Profile { get; private set; }

    /// <summary>
    /// Gets the width of the video stream (if applicable).
    /// </summary>
    public int? Width { get; private set; } = null;

    /// <summary>
    /// Gets the height of the video stream (if applicable).
    /// </summary>
    public int? Height { get; private set; } = null;

    /// <summary>
    /// Gets the aspect ratio of the video stream (if applicable).
    /// </summary>
    public string AspectRatio { get; private set; }

    /// <summary>
    /// Gets the pixel format of the video stream (if applicable).
    /// </summary>
    public string PixelFormat { get; private set; }

    /// <summary>
    /// Gets the level of the video stream (if applicable).
    /// </summary>
    public int Level { get; private set; }

    /// <summary>
    /// Gets the color range of the video stream (if applicable).
    /// </summary>
    public string ColorRange { get; private set; }

    /// <summary>
    /// Gets the frame rate of the video stream (if applicable).
    /// </summary>
    public double? FrameRate { get; private set; } = null;

    /// <summary>
    /// Gets the total number of frames in the video stream (if applicable).
    /// </summary>
    public long? Frames { get; private set; } = null;

    /// <summary>
    /// Gets the bits per second (BPS) of the video stream (if applicable).
    /// </summary>
    public long? BPS { get; private set; } = null;

    /// <summary>
    /// Gets the duration of the media stream.
    /// </summary>
    public TimeSpan? Duration { get; private set; } = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="FFMediaStream"/> struct with the specified properties.
    /// </summary>
    /// <param name="index">The index of the media stream.</param>
    /// <param name="codecName">The name of the codec used for encoding this stream.</param>
    /// <param name="codecLongName">The long name of the codec used for encoding this stream.</param>
    /// <param name="codecType">The type of codec used for encoding this stream (e.g., audio, video).</param>
    /// <param name="profile">The profile of the codec used for encoding this stream.</param>
    /// <param name="width">The width of the video stream (if applicable).</param>
    /// <param name="height">The height of the video stream (if applicable).</param>
    /// <param name="aspectRatio">The aspect ratio of the video stream (if applicable).</param>
    /// <param name="pixelFormat">The pixel format of the video stream (if applicable).</param>
    /// <param name="level">The level of the video stream (if applicable).</param>
    /// <param name="colorRange">The color range of the video stream (if applicable).</param>
    /// <param name="frameRate">The frame rate of the video stream (if applicable).</param>
    /// <param name="frames">The total number of frames in the video stream (if applicable).</param>
    /// <param name="bPS">The bits per second (BPS) of the video stream (if applicable).</param>
    /// <param name="duration">The duration of the media stream.</param>
    public FFMediaStream(int index, string codecName, string codecLongName, string codecType, string profile, int? width, int? height, string aspectRatio, string pixelFormat, int level, string colorRange, double? frameRate, long? frames, long? bPS, TimeSpan? duration)
    {
        Index = index;
        CodecName = codecName ?? throw new ArgumentNullException(nameof(codecName));
        CodecLongName = codecLongName ?? throw new ArgumentNullException(nameof(codecLongName));
        CodecType = codecType ?? throw new ArgumentNullException(nameof(codecType));
        Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        Width = width;
        Height = height;
        AspectRatio = aspectRatio ?? throw new ArgumentNullException(nameof(aspectRatio));
        PixelFormat = pixelFormat ?? throw new ArgumentNullException(nameof(pixelFormat));
        Level = level;
        ColorRange = colorRange ?? throw new ArgumentNullException(nameof(colorRange));
        FrameRate = frameRate;
        Frames = frames;
        BPS = bPS;
        Duration = duration;
    }
}