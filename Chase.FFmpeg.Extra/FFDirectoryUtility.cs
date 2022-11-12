namespace Chase.FFmpeg.Extra;

internal static class FFDirectoryUtility
{

    public static IReadOnlyCollection<string> GetFiles(string path, bool recursive, Func<string, bool> comparison)
    {
        List<string> files = new();

        Parallel.ForEach(Directory.GetFileSystemEntries(path), i =>
        {
            try
            {
                if (new FileInfo(i).Attributes.HasFlag(FileAttributes.Directory))
                {
                    if (recursive)
                    {
                        files.AddRange(GetFiles(i, recursive, comparison));
                    }
                }
                else
                {
                    if (comparison.Invoke(i))
                    {
                        files.Add(i);
                    }
                }
            }
            catch
            { }
        });

        return files;
    }

}
