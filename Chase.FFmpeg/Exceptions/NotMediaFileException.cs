namespace Chase.FFmpeg.Exceptions;

/// <summary>
/// When a file is not a media file!
/// </summary>
public class NotMediaFileException : IOException
{
    internal NotMediaFileException(string file) : base($"File is not a media file: {file}")
    {

    }
}
