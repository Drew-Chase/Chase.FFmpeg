/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.FFmpeg.Downloader;

/// <summary>
/// an object representation of an ffmpeg installation
/// </summary>
public sealed class FFInstallation
{
    /// <summary>
    /// The absolute path to the ffmpeg executable <br/>
    /// <code>Example: /path/to/ffmpeg.exe</code>
    /// </summary>
    public string FFmpeg { get; set; } = "";

    /// <summary>
    /// The currently installed version of ffmpeg
    /// </summary>
    public string Version { get; set; } = "";

    /// <summary>
    /// The absolute path to the ffprobe executable <br/>
    /// <code>Example: /path/to/ffprobe.exe</code>
    /// </summary>
    public string FFProbe { get; set; } = "";

    /// <summary> The absolute path to the ffplay executable <br/> <code>Example: /path/to/ffplay.exe</code>
    public string FFPlay { get; set; } = "";
}