
using Chase.FFmpeg.Info;

namespace Chase.FFmpeg.Extra;

internal static class FFDirectoryUtility
{

    public static ICollection<FFMediaInfo> GetFiles(string path, bool recursive, Func<string, bool> comparison)
    {
        List<FFMediaInfo> files = new();
        foreach (string file in Directory.GetFileSystemEntries(path))
        {
            try
            {
                if (new FileInfo(file).Attributes.HasFlag(FileAttributes.Directory))
                {
                    if (recursive)
                    {
                        files.AddRange(GetFiles(file, recursive, comparison));
                    }
                }
                else
                {
                    if (comparison.Invoke(file))
                    {
                        files.Add(new(file));
                    }
                }
            }
            catch
            { }
        }

        return files;
    }

    public static ICollection<FFMediaInfo> GetFilesAsync(string path, bool recursive, Func<string, bool> comparison)
    {
        List<FFMediaInfo> files = new();

        Parallel.ForEach(Directory.GetFileSystemEntries(path), file =>
        {
            try
            {
                if (new FileInfo(file).Attributes.HasFlag(FileAttributes.Directory))
                {
                    if (recursive)
                    {
                        files.AddRange(GetFilesAsync(file, recursive, comparison));
                    }
                }
                else
                {
                    if (comparison.Invoke(file))
                    {
                        files.Add(new(file));
                    }
                }
            }
            catch
            { }
        });

        return files;
    }
}
