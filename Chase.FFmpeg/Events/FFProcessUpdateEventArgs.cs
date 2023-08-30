/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.FFmpeg.Events;

/// <summary>
/// Runs when ffmpeg changes conversion status
/// </summary>
public class FFProcessUpdateEventArgs : EventArgs
{
    /// <summary>
    /// The bitrate that the video is being processed at
    /// </summary>
    public float AverageBitrate { get; set; }

    /// <summary>
    /// The number of frames already processed
    /// </summary>
    public uint FramesProcessed { get; set; }

    /// <summary>
    /// The percentage of the video has been processed
    /// </summary>
    public float Percentage { get; set; }

    /// <summary>
    /// The speed that the video is processing at
    /// </summary>
    public float Speed { get; set; }
}