/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using System.Runtime.InteropServices;

namespace Chase.FFmpeg.Capture.Utilities;

internal static class DisplayInfo
{
    public static class Windows
    {
        public delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);

        public const int MONITOR_DEFAULTTOPRIMARY = 1;
        public const int MONITORINFOF_PRIMARY = 1;

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfoA(IntPtr hMonitor, ref MonitorInfoEx lpmi);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromPoint(Point pt, int dwFlags);
    }

    public static class Unix
    {
        [DllImport("libX11.so.6")]
        public static extern IntPtr XOpenDisplay(IntPtr display);

        [DllImport("libX11.so.6")]
        public static extern int XScreenCount(IntPtr display);

        [DllImport("libX11.so.6")]
        public static extern IntPtr XScreenOfDisplay(IntPtr display, int screen_number);

        [DllImport("libX11.so.6")]
        public static extern string XDisplayString(IntPtr display);

        [DllImport("libX11.so.6")]
        public static extern int XDisplayWidth(IntPtr display, int screen_number);

        [DllImport("libX11.so.6")]
        public static extern int XDisplayHeight(IntPtr display, int screen_number);
    }
}