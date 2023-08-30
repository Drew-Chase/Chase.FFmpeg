/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.FFmpeg.Capture.Utilities;

public struct Display
{
    public string DeviceName { get; set; }
    public Rect MonitorRect { get; set; }
    public Rect WorkAreaRect { get; set; }
    public bool IsPrimary { get; set; }
    public DisplayResolution Resolution { get; set; }
}