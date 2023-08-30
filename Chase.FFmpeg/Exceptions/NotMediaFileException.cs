/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.FFmpeg.Exceptions;

/// <summary>
/// When a file is not a media file!
/// </summary>
public class NotMediaFileException : IOException
{
    internal NotMediaFileException(string file) : base($"File is not a media file: {file}")
    {
    }
}