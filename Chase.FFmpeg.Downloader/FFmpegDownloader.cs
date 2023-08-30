/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Chase.FFmpeg.Downloader.Networking;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO.Compression;

namespace Chase.FFmpeg.Downloader;

/// <summary>
/// Handles downloading the latest version of ffmpeg
/// </summary>
public sealed class FFmpegDownloader
{
    /// <summary>
    /// Singleton pattern for FFmpegDownloader
    /// </summary>
    public static readonly FFmpegDownloader Instance = Instance ??= new();

    private string _directory = "", _version = "";

    /// <summary>
    /// The current loaded ffmpeg installation, see: <seealso cref="GetLatest(string)"/>
    /// </summary>
    public FFInstallation LoadedInstallation { get; private set; }

    private FFmpegDownloader()
    {
        LoadedInstallation = new FFInstallation
        {
            FFmpeg = GetFromEnvironment("ffmpeg") ?? "",
            FFPlay = GetFromEnvironment("ffplay") ?? "",
            FFProbe = GetFromEnvironment("ffprobe") ?? "",
            Version = GetCurrentVersion() ?? "",
        };
    }

    /// <summary>
    /// Gets the current version or creates a version file if none is found.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentVersion()
    {
        string version_file = Path.Combine(_directory, "version.json");
        if (!File.Exists(version_file))
        {
            CreateVersionFile();
            return FFUrlParser.Instance.Version;
        }
        try
        {
            using FileStream fs = new(Path.Combine(_directory, "version.json"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader reader = new(fs);
            return JObject.Parse(reader.ReadToEnd())?["version"]?.ToObject<string>() ?? "";
        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// Downloads the latest version of ffmpeg if one is needed. <br/> Otherwise does nothing...
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public async Task<FFInstallation> GetLatest(string directory)
    {
        _directory = directory = Directory.CreateDirectory(directory).FullName;
        _version = Path.Combine(directory, "version.json");
        LoadedInstallation.Version = FFUrlParser.Instance.Version;
        if (File.Exists(_version))
        {
            // Checks if the remote version is different from the local one...
            if (GetCurrentVersion() != FFUrlParser.Instance.Version)
            {
                bool _ffmpeg_exists = Directory.GetFiles(directory, "ffmpeg*", SearchOption.TopDirectoryOnly).Any();
                bool _ffprobe_exists = Directory.GetFiles(directory, "ffprobe*", SearchOption.TopDirectoryOnly).Any();
                bool _ffplay_exists = Directory.GetFiles(directory, "ffplay*", SearchOption.TopDirectoryOnly).Any();
                if (_ffmpeg_exists)
                {
                    File.Delete(Directory.GetFiles(directory, "ffmpeg*", SearchOption.TopDirectoryOnly).First());
                }
                if (_ffprobe_exists)
                {
                    File.Delete(Directory.GetFiles(directory, "ffprobe*", SearchOption.TopDirectoryOnly).First());
                }
                if (_ffplay_exists)
                {
                    File.Delete(Directory.GetFiles(directory, "ffplay*", SearchOption.TopDirectoryOnly).First());
                }
            }
        }

        bool ffmpeg_exists = Directory.GetFiles(directory, "ffmpeg*", SearchOption.TopDirectoryOnly).Any();
        bool ffprobe_exists = Directory.GetFiles(directory, "ffprobe*", SearchOption.TopDirectoryOnly).Any();
        bool ffplay_exists = Directory.GetFiles(directory, "ffplay*", SearchOption.TopDirectoryOnly).Any();

        if (!ffmpeg_exists && FFUrlParser.Instance.FFmpeg != null)
        {
            string ffmpeg_archive = await GetArchive(FFUrlParser.Instance.FFmpeg);
            LoadedInstallation.FFmpeg = Unzip(ffmpeg_archive);
        }
        if (!ffprobe_exists && FFUrlParser.Instance.FFprobe != null)
        {
            string ffprobe_archive = await GetArchive(FFUrlParser.Instance.FFprobe);
            LoadedInstallation.FFProbe = Unzip(ffprobe_archive);
        }
        if (!ffplay_exists && FFUrlParser.Instance.FFPlay != null)
        {
            string ffplay_archive = await GetArchive(FFUrlParser.Instance.FFPlay);
            LoadedInstallation.FFPlay = Unzip(ffplay_archive);
        }

        CreateVersionFile();

        return LoadedInstallation;
    }

    /// <summary>
    /// Gets the path of the command executable.
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    private static string? GetFromEnvironment(string cmd)
    {
        string? result = null;
        string whichCommand = OperatingSystem.IsWindows() ? "where" : "which";

        using Process process = new();
        process.StartInfo = new()
        {
            FileName = whichCommand,
            Arguments = cmd,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        process.EnableRaisingEvents = true;
        process.OutputDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                result = args.Data;
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        return result;
    }

    /// <summary>
    /// Unzips archive in-place.
    /// </summary>
    /// <param name="archive"></param>
    private static string Unzip(string archive)
    {
        string path = "";
        using (FileStream fs = new(archive, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using ZipArchive zip = new(fs, ZipArchiveMode.Read, false);
            DirectoryInfo? parentInfo = Directory.GetParent(archive);
            if (parentInfo != null)
            {
                zip.ExtractToDirectory(parentInfo.FullName);
                path = Path.Combine(parentInfo.FullName, zip.Entries.First().FullName);
            }
        }
        File.Delete(archive);
        return path;
    }

    /// <summary>
    /// Creates the version file with the remote version
    /// </summary>
    private void CreateVersionFile()
    {
        using FileStream fs = new(Path.Combine(_directory, "version.json"), FileMode.Create, FileAccess.Write, FileShare.Read);
        using StreamWriter writer = new(fs);
        writer.Write(System.Text.Json.JsonSerializer.Serialize(new
        {
            version = FFUrlParser.Instance.Version
        }));
    }

    /// <summary>
    /// Downloads the archive file from the remote server.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private async Task<string> GetArchive(Uri url)
    {
        string archive = Path.Combine(_directory, $"{DateTime.Now.Ticks}.zip");
        using HttpClient client = new();
        using FileStream fs = new(archive, FileMode.Create, FileAccess.Write, FileShare.None);
        using Stream dl_stream = await client.GetStreamAsync(url);
        await dl_stream.CopyToAsync(fs);
        return archive;
    }
}