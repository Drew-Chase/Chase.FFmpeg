
namespace Chase.FFmpeg.Extra;

internal static class FFDirectoryUtility
{

    public static ICollection<string> GetFilesAsync(string path, bool recursive, Func<string, bool> comparison)
    {
        List<string> files = new();

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
                        files.Add(file);
                    }
                }
            }
            catch
            { }
        });

        return files;
    }

    public static ICollection<string> GetFiles(string path, bool recursive, Func<string, bool> comparison)
    {
        List<string> files = new();
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
                        files.Add(file);
                    }
                }
            }
            catch
            { }
        }

        return files;
    }

}
