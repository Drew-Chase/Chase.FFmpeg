// LFInteractive LLC. - All Rights Reserved
using Chase.FFmpeg.Downloader;
using Chase.FFmpeg.Info;
using Serilog;

namespace Chase.FFmpeg.Client;

internal class Program
{
    private static void Main(string[] args)
    {
        ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        log.Information("Welcome to BetterFFmpeg");
        //if (string.IsNullOrWhiteSpace(FFmpegDownloader.Instance.FFmpegVersion))
        //    log.Warning("Downloading the latest version of ffmpeg...");
        FFmpegDownloader.Instance.GetLatest(Path.Combine(Path.GetTempPath(), "Chase.FFMpeg")).Wait();
        string file = "";
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-i" && i + 1 <= args.Length)
            {
                file = Path.GetFullPath(args[i + 1]);
                break;
            }
        }
        if (!string.IsNullOrEmpty(file))
        {
            if (File.Exists(file))
            {
                double percentage = 0d;
                float speed = 0f;
                System.Timers.Timer timer = new(1000)
                {
                    Enabled = true,
                    AutoReset = true,
                };

                FileInfo fileInfo = new(file);

                timer.Elapsed += (s, e) =>
                {
                    if (Console.CursorTop > 0)
                    {
                        Console.CursorTop--;
                    }
                    Console.CursorLeft = 0;
                    for (int i = 0; i < Console.WindowWidth; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.CursorLeft = 0;
                    log.Information("Percentage: {percentage} | Speed: {speed}x | Input: {file}", percentage.ToString("p2"), speed, fileInfo.Name);
                };

                timer.Start();

                FFMediaInfo info = new(file);
                log.Information("Processing {FILE}", file);
                FFProcessHandler.ExecuteFFmpeg(string.Join(' ', args), info, null, (s, e) =>
                {
                    percentage = e.Percentage;
                    speed = e.Speed;
                });
            }
            else
            {
                log.Error("Input file does NOT exist on disk: {FILE}", file);
            }
        }
        else
        {
            log.Error("Input file cannot be null or empty");
        }
    }
}