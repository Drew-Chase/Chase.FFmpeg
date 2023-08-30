/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

// Ignore Spelling: Screenshot

using Chase.FFmpeg.Capture.Data;
using Chase.FFmpeg.Capture.Utilities;
using Chase.FFmpeg.Exceptions;
using System.Diagnostics;
using System.Text;

namespace Chase.FFmpeg.Capture;

public static class DisplayCapture
{
    public static bool TakeScreenshot(WindowedProcess window, string output, bool overwrite = true) => TakeScreenshot(input: $"\"title={window.Title}\"", output: output, overwrite: overwrite);

    public static bool TakeScreenshot(Display display, string output, bool overwrite = true) => TakeScreenshot(new Point(display.MonitorRect.Left, display.MonitorRect.Top), display.Resolution, output, overwrite);

    public static bool TakeScreenshot(Display display, Point location, DisplayResolution resolution, string output, bool overwrite = true) => TakeScreenshot(new Point(display.MonitorRect.Left + location.x, display.MonitorRect.Top + location.y), new DisplayResolution(display.Resolution.Width + resolution.Width, display.Resolution.Height + resolution.Height), output, overwrite);

    public static bool TakeScreenshot(Point location, DisplayResolution resolution, string output, bool overwrite = true) => TakeScreenshot(output, overwrite: overwrite, location: location, resolution: resolution);

    public static bool TakeScreenshot(string output, bool overwrite = true) => TakeScreenshot(output, overwrite: overwrite);

    private static bool TakeScreenshot(string output, string input = "", bool overwrite = false, Point? location = null, DisplayResolution? resolution = null)
    {
        StringBuilder argumentBuilder = new();
        if (overwrite)
        {
            argumentBuilder.Append("-y ");
        }
        argumentBuilder.Append("-f ");
        if (OperatingSystem.IsWindows())
        {
            argumentBuilder.Append("gdigrab -i ");
        }
        else if (OperatingSystem.IsLinux())
        {
            argumentBuilder.Append("x11grab -i ");
        }
        else if (OperatingSystem.IsMacOS())
        {
            argumentBuilder.Append("avfoundation -i ");
        }
        else
        {
            throw new InvalidOperatingSystemException();
        }
        if (!string.IsNullOrWhiteSpace(input))
        {
            argumentBuilder.Append(input);
        }
        else
        {
            if (OperatingSystem.IsWindows())
            {
                argumentBuilder.Append("desktop");
            }
            else if (OperatingSystem.IsLinux())
            {
                argumentBuilder.Append(":0.0");
            }
            else if (OperatingSystem.IsMacOS())
            {
                argumentBuilder.Append("\"1\"");
            }
            else
            {
                throw new InvalidOperatingSystemException();
            }
        }

        if (location != null && resolution != null)
        {
            // Append the initial part of the command
            argumentBuilder.Append(" -vf ");

            // Define the values for cropping
            int width = resolution.Value.Width;
            int height = resolution.Value.Height;
            int x = location.Value.x;
            int y = location.Value.y;

            // Append the cropping filter
            argumentBuilder.Append("\"crop=");
            argumentBuilder.Append(width);
            argumentBuilder.Append(':');
            argumentBuilder.Append(height);
            argumentBuilder.Append(':');
            argumentBuilder.Append(x);
            argumentBuilder.Append(':');
            argumentBuilder.Append(y);
            argumentBuilder.Append('"');
        }

        argumentBuilder.Append(" -frames:v 1 ");
        argumentBuilder.Append(output);

        using Process process = FFProcessHandler.ExecuteFFmpeg(argumentBuilder.ToString());
        Console.WriteLine("ffmpeg " + argumentBuilder.ToString());
        return process.ExitCode == 0;
    }
}