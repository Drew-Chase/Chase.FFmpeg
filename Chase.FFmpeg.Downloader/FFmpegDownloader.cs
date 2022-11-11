using Chase.FFmpeg.Downloader.Networking;
using Newtonsoft.Json.Linq;
using System.IO.Compression;

namespace Chase.FFmpeg.Downloader;

public sealed class FFmpegDownloader
{
    public static FFmpegDownloader Instance = Instance ??= new();
    private string _directory, _version;
    public string FFmpegExecutable { get; private set; }
    public string FFprobeExecutable { get; private set; }
    public string FFmpegVersion { get; private set; }
    /// <summary>
    /// Downloads the latest version of ffmpeg if one is needed. <br /> Otherwise does nothing...
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public async Task GetLatest(string directory)
    {
        _directory = directory = Directory.CreateDirectory(directory).FullName;
        _version = Path.Combine(directory, "version.json");
        FFmpegVersion = FFUrlParser.Instance.Version;
        if (File.Exists(_version))
        {
            /// Checks if the remote version is different from the local one...
            if (GetCurrentVersion() != FFUrlParser.Instance.Version)
            {
                bool _ffmpeg_exists = Directory.GetFiles(directory, "ffmpeg*", SearchOption.TopDirectoryOnly).Any();
                bool _ffprobe_exists = Directory.GetFiles(directory, "ffprobe*", SearchOption.TopDirectoryOnly).Any();
                if (_ffmpeg_exists)
                {
                    File.Delete(Directory.GetFiles(directory, "ffmpeg*", SearchOption.TopDirectoryOnly).First());
                }
                if (_ffprobe_exists)
                {
                    File.Delete(Directory.GetFiles(directory, "ffprobe*", SearchOption.TopDirectoryOnly).First());
                }
            }
        }

        bool ffmpeg_exists = Directory.GetFiles(directory, "ffmpeg*", SearchOption.TopDirectoryOnly).Any();
        bool ffprobe_exists = Directory.GetFiles(directory, "ffprobe*", SearchOption.TopDirectoryOnly).Any();

        if (!ffmpeg_exists)
        {
            string ffmpeg_archive = await GetArchive(FFUrlParser.Instance.FFmpeg);
            Unzip(ffmpeg_archive);
        }
        if (!ffprobe_exists)
        {
            string ffprobe_archive = await GetArchive(FFUrlParser.Instance.FFprobe);
            Unzip(ffprobe_archive);
        }

        CreateVersionFile();

        FFmpegExecutable = Directory.GetFiles(directory, "ffmpeg*", SearchOption.TopDirectoryOnly).First();
        FFprobeExecutable = Directory.GetFiles(directory, "ffprobe*", SearchOption.TopDirectoryOnly).First();
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

    /// <summary>
    /// Unzips archive in-place.
    /// </summary>
    /// <param name="archive"></param>
    private static void Unzip(string archive)
    {
        using (FileStream fs = new(archive, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using ZipArchive zip = new(fs, ZipArchiveMode.Read, false);
            DirectoryInfo? parentInfo = Directory.GetParent(archive);
            if (parentInfo != null)
            {
                zip.ExtractToDirectory(parentInfo.FullName);
            }
        }
        File.Delete(archive);
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
            return (string)JObject.Parse(reader.ReadToEnd())["version"];
        }
        catch
        {
            return "";
        }
    }
}
