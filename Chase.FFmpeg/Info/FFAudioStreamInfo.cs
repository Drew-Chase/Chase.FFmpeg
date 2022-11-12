namespace Chase.FFmpeg.Info;

/// <summary>
/// Stores information about the audio stream
/// </summary>
public struct FFAudioStreamInfo
{
    internal FFAudioStreamInfo(string codec, uint sampleRate, string sampleFormat, string channelLayout, uint size, uint channels)
    {
        Codec = codec;
        SampleRate = sampleRate;
        SampleFormat = sampleFormat;
        ChannelLayout = channelLayout;
        Size = size;
        Channels = channels;
    }

    /// <summary>
    /// The channel layout of the audio stream
    /// </summary>
    public string ChannelLayout { get; private set; }

    /// <summary>
    /// The number of channels in the audio stream
    /// </summary>
    public uint Channels { get; private set; }

    /// <summary>
    /// The codec that the audio stream was encoded with
    /// </summary>
    public string Codec { get; private set; }
    /// <summary>
    /// The sample format of the audio stream
    /// </summary>
    public string SampleFormat { get; private set; }

    /// <summary>
    /// The sample rate of the audio stream
    /// </summary>
    public uint SampleRate { get; private set; }
    /// <summary>
    /// The size in bytes of the audio stream
    /// </summary>
    public uint Size { get; private set; }
}
