/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using System.Runtime.InteropServices;

namespace Chase.FFmpeg.Exceptions;

public class InvalidOperatingSystemException : Exception
{
    public InvalidOperatingSystemException() : base($"Operating System not supported: {RuntimeInformation.OSDescription}")
    {
    }
}