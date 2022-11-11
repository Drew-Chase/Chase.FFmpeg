using Chase.FFmpeg.Downloader.Environment;
using Newtonsoft.Json.Linq;

namespace Chase.FFmpeg.Downloader.Networking;

internal sealed class FFUrlParser
{
    public readonly static FFUrlParser Instance = Instance ??= new();
    public readonly Uri FFmpeg, FFprobe;
    public readonly string Version;
    private FFUrlParser()
    {
        JObject json = GetJson();

        Version = (string)json["version"];
        FFmpeg = new((string)json["bin"][FFOSProvider.Name]["ffmpeg"]);
        FFprobe = new((string)json["bin"][FFOSProvider.Name]["ffprobe"]);
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
