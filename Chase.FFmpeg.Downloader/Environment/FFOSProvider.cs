using System.Runtime.InteropServices;
using System.Text;

namespace Chase.FFmpeg.Downloader.Environment;

/// <summary>
/// Gives utility variables for the current operating system
/// </summary>
internal static class FFOSProvider
{

    /// <summary>
    /// The ffmpeg friendly name for the systems cpu architecture and Operating System name
    /// </summary>
    public static string Name
    {
        get
        {
            StringBuilder name = new();

            if (OperatingSystem.IsLinux())
            {
                name.Append("linux");
                if (IsARM)
                {
                    if (IsARM64)
                    {
                        name.Append("-arm64");
                    }
                    else
                    {
                        // ARM hardware float / ARM 32 bit processors
                        name.Append("-armhf");
                    }
                }
            }
            else if (OperatingSystem.IsWindows())
            {
                name.Append("windows");
            }
            else if (OperatingSystem.IsMacOS())
            {
                name.Append("osx");
            }


            if (Is64)
            {
                name.Append("-64");
            }
            else
            {
                name.Append("-32");
            }

            return name.ToString();
        }
    }

    /// <summary>
    /// If the processor is ARM based or not
    /// </summary>
    public static bool IsARM => IsARM64 || IsARM32;
    /// <summary>
    /// If the processor is ARM 64 bit or not
    /// </summary>
    public static bool IsARM64 => RuntimeInformation.ProcessArchitecture == Architecture.Arm64;
    /// <summary>
    /// If the processor is ARM 32 bit or not
    /// </summary>
    public static bool IsARM32 => RuntimeInformation.ProcessArchitecture == Architecture.Arm64;
    /// <summary>
    /// If the processor is AMD 64 bit
    /// </summary>
    public static bool Is64 => RuntimeInformation.ProcessArchitecture == Architecture.X64;
    /// <summary>
    /// If the processor is Intel i386 32 bit processor
    /// </summary>
    public static bool Is32 => RuntimeInformation.ProcessArchitecture == Architecture.X86;

}