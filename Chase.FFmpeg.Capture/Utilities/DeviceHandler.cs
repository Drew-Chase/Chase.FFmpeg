/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Chase.FFmpeg.Capture.Data;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static Chase.FFmpeg.Capture.Utilities.DisplayInfo;

namespace Chase.FFmpeg.Capture.Utilities;

public static class DeviceHandler
{
    public static InputDevice[] GetInputDevices()
    {
        List<InputDevice> inputDevices = new();
        StringBuilder responseBuilder = new();
        FFProcessHandler.ExecuteFFmpeg("-list_devices true -f dshow -i dummy", data_handler: (s, e) =>
        {
            string? data = e.Data;
            if (!string.IsNullOrWhiteSpace(data) && data.StartsWith('['))
            {
                int end = data.IndexOf(']') + 1;
                if (end > 0)
                {
                    string line = data[end..].Trim();
                    responseBuilder.AppendLine(line);
                }
            }
        });

        var regex = new Regex(@"""(.*?)""\s*\((.*?)\)[\r\n\s]*Alternative name\s*""(.*?)""", RegexOptions.Singleline);
        var matches = regex.Matches(responseBuilder.ToString());

        foreach (Match match in matches)
        {
            if (match.Groups.Count == 4)
            {
                string display = match.Groups[1].Value.Trim();
                string typeString = match.Groups[2].Value.Trim().ToLower();
                string handle = match.Groups[3].Value.Trim();

                InputType type = InputType.None;
                if (typeString == "video")
                {
                    type = InputType.Video;
                }
                else if (typeString == "audio")
                {
                    type = InputType.Audio;
                }

                inputDevices.Add(new InputDevice(display, type, handle));
            }
        }

        return inputDevices.ToArray();
    }

    public static List<WindowedProcess> GetWindowedProcesses()
    {
        List<WindowedProcess> result = new();

        foreach (Process process in Process.GetProcesses())
        {
            try
            {
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    result.Add(new(process));
                }
            }
            catch (Win32Exception)
            {
            }
        }

        return result;
    }

    public static WindowedProcess? GetWindowedProcessByName(string name)
    {
        foreach (Process process in Process.GetProcesses())
        {
            if (process.MainWindowHandle != IntPtr.Zero && process.MainWindowTitle.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return new WindowedProcess(process);
            }
        }

        return null;
    }

    public static string[] GetWindowTitles()
    {
        List<string> result = new();
        var processes = GetWindowedProcesses();
        foreach (string title in processes.Select(i => i.Title))
        {
            if (!string.IsNullOrEmpty(title))
                result.Add(title);
        }
        return result.ToArray();
    }

    public static Display[] GetConnectedDisplays()
    {
        List<Display> displays = new();

        if (OperatingSystem.IsWindows())
        {
            bool callback(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData)
            {
                MonitorInfoEx monitorInfo = new();
                monitorInfo.cbSize = Marshal.SizeOf(monitorInfo);

                if (Windows.GetMonitorInfoA(hMonitor, ref monitorInfo))
                {
                    Display display = new()
                    {
                        DeviceName = monitorInfo.szDevice,
                        MonitorRect = lprcMonitor,
                        WorkAreaRect = monitorInfo.rcWork,
                        IsPrimary = ((monitorInfo.dwFlags & Windows.MONITORINFOF_PRIMARY) != 0),
                        Resolution = new DisplayResolution()
                        {
                            Width = lprcMonitor.Right - lprcMonitor.Left,
                            Height = lprcMonitor.Bottom - lprcMonitor.Top,
                        }
                    };

                    displays.Add(display);
                }

                return true;
            }

            Windows.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, IntPtr.Zero);
        }
        else
        {
            // Handle Unix
        }

        return displays.ToArray();
    }
}