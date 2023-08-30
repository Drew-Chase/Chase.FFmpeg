/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

namespace Chase.FFmpeg.Capture.Data;

public struct InputDevice
{
    public string Display { get; set; }
    public InputType Type { get; set; }
    public string Handle { get; set; }

    public InputDevice(string display, InputType type, string handle)
    {
        Display = display ?? throw new ArgumentNullException(nameof(display));
        Type = type;
        Handle = handle ?? throw new ArgumentNullException(nameof(handle));
    }
}