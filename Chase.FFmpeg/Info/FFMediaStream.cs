/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.FFmpeg.Info;

public struct FFMediaStream
{
    public int Index { get; private set; }

    public string CodecName { get; private set; }

    public string CodecLongName { get; private set; }

    public string CodecType { get; private set; }

    public string Profile { get; private set; }

    public int? Width { get; private set; } = null;

    public int? Height { get; private set; } = null;

    public string AspectRatio { get; private set; }

    public string PixelFormat { get; private set; }

    public int Level { get; private set; }

    public string ColorRange { get; private set; }

    public double? FrameRate { get; private set; } = null;

    public long? Frames { get; private set; } = null;

    public long? BPS { get; private set; } = null;

    public TimeSpan? Duration { get; private set; } = null;

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