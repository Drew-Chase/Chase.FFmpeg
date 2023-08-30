/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.FFmpeg.Capture.Utilities;

public struct DisplayResolution
{
    public int Width { get; set; }
    public int Height { get; set; }

    public DisplayResolution(int width, int height)
    {
        Width = width;
        Height = height;
    }
}