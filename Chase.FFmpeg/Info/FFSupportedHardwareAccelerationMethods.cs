namespace Chase.FFmpeg.Info;

/// <summary>
/// Adds functions getting Supported Hardware Acceleration Methods
/// </summary>
public class FFSupportedHardwareAccelerationMethods
{
    /// <summary>
    /// Singleton pattern for FFSupportedHardwareAccelerationMethods
    /// </summary>
    public static readonly FFSupportedHardwareAccelerationMethods Instance = Instance ??= new();

    /// <summary>
    /// An array of supported methods
    /// </summary>
    public string[] Methods { get; private set; }

    private FFSupportedHardwareAccelerationMethods()
    {
        List<string> methods = new();
        FFProcessHandler.ExecuteFFmpeg("-hide_banner -hwaccels", null, (s, e) =>
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
