/*
    Chase FFmpeg - LFInteractive LLC. 2021-2024
    Chase FFmpeg is a ffmpeg wrapper for c#. Includes the ability to download, execute and manipulate ffmpeg, ffprobe and ffplay.
    Licensed under GPL-3.0
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
*/

using Chase.FFmpeg.Info;

namespace Chase.FFmpeg.Extra;

/// <summary>
/// Static methods for handling video files
/// </summary>
public static class FFVideoUtility
{
    /// <summary>
    /// Returns an array of all video file extensions
    /// </summary>
    public static readonly string[] video_extension = { "webm", "mkv", "flv", "flv", "vob", "ogv", "ogg", "drc", "gif", "gifv", "mng", "avi", "mts", "m2ts", "ts", "mov", "qt", "wmv", "yuv", "rm", "rmvb", "viv", "asf", "amv", "mp4", "m4p", "m4v", "mpg", "mp2", "mpeg", "mpe", "mpv", "mpg", "mpeg", "m2v", "m4v", "svi", "3gp", "3g2", "mxf", "roq", "nsv", "flv", "f4v", "f4p", "f4a", "f4b", };

    /// <summary>
    /// Gets all files with <seealso cref="video_extension">Video Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<string> GetFiles(string path, bool recursive = false) => FFDirectoryUtility.GetFiles(path, recursive, item => HasVideoExtension(item));

    /// <summary>
    /// Gets all files asynchronous with <seealso cref="video_extension">Video Extension</seealso>
    /// in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<string> GetFilesAsync(string path, bool recursive = false) => FFDirectoryUtility.GetFilesAsync(path, recursive, item => HasVideoExtension(item));

    /// <summary>
    /// Gets all files with <seealso cref="video_extension">Video Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<FFMediaInfo> GetMediaFiles(string path, bool recursive = false) => FFDirectoryUtility.GetMediaFiles(path, recursive, item => HasVideoExtension(item));

    /// <summary>
    /// Gets all files asynchronous with <seealso cref="video_extension">Video Extension</seealso>
    /// in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<FFMediaInfo> GetMediaFilesAsync(string path, bool recursive = false) => FFDirectoryUtility.GetMediaFilesAsync(path, recursive, item => HasVideoExtension(item));

    /// <summary>
    /// Checks if file has a extension matching the <seealso cref="video_extension">Video
    /// Extensions</seealso> array
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool HasVideoExtension(string path) => video_extension.Contains(new FileInfo(path).Extension.Trim('.'));
}