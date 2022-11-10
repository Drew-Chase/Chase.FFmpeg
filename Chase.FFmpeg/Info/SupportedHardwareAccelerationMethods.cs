namespace Chase.FFmpeg.Info;

public class SupportedHardwareAccelerationMethods
{
    public static SupportedHardwareAccelerationMethods Instance = Instance ??= new();
    public string[] Methods { get; private set; }

    private SupportedHardwareAccelerationMethods()
    {
        List<string> methods = new();
        FFProcessHandler.ExecuteFFmpeg("-hide_banner -hwaccels", (s, e) =>
        {
            string? content = e.Data;
            if (!string.IsNullOrWhiteSpace(content))
            {
                if (!content.ToLower().StartsWith("hardware acceleration methods"))
                {
                    methods.Add(content);
                }
            }
        }, null);
        Methods = methods.ToArray();
    }
}
