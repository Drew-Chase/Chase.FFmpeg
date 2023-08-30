// LFInteractive LLC. 2021-2024﻿
using Chase.FFmpeg.Downloader.Environment;
using Newtonsoft.Json.Linq;

namespace Chase.FFmpeg.Downloader.Networking;

internal sealed class FFUrlParser
{
    public static readonly FFUrlParser Instance = Instance ??= new();
    public readonly Uri? FFmpeg, FFprobe, FFPlay;
    public readonly string Version;

    private FFUrlParser()
    {
        JObject json = GetJson();

        Version = json["version"]?.ToObject<string>() ?? "";
        FFmpeg = json["bin"]?[FFOSProvider.Name]?["ffmpeg"]?.ToObject<Uri>();
        FFprobe = json["bin"]?[FFOSProvider.Name]?["ffprobe"]?.ToObject<Uri>();
        if (OperatingSystem.IsWindows() || OperatingSystem.IsMacOS())
        {
            FFPlay = json["bin"]?[FFOSProvider.Name]?["ffplay"]?.ToObject<Uri>();
        }
    }

    private JObject GetJson()
    {
        using HttpClient client = new();
        using HttpResponseMessage message = client.GetAsync("https://ffbinaries.com/api/v1/version/latest").Result;

        if (message.IsSuccessStatusCode)
        {
            return JObject.Parse(message.Content.ReadAsStringAsync().Result);
        }
        throw new System.Net.WebException($"FFBinaries api returned with status code {message.StatusCode}");
    }
}