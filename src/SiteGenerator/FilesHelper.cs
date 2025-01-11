using System.Security.Cryptography;

namespace SiteGenerator;

internal static class FilesHelper
{
    public static (IReadOnlyList<FileInfo> Files, IReadOnlyList<DirectoryInfo> Directories) DiscoverFiles(string dir)
    {
        if (!Directory.Exists(dir))
        {
            throw new DirectoryNotFoundException(
                $"Directory {dir} was not found.");
        }

        List<FileInfo> files = [];
        List<DirectoryInfo> dirs = [];
        _ = DiscoverFilesImpl(dir, files, dirs);
        return (files, dirs);
    }

    public static IEnumerable<FileInfo> DiscoverFilesImpl(
        string dir, List<FileInfo> files, List<DirectoryInfo> dirs)
    {
        IEnumerable<string> subdirs = Directory.EnumerateDirectories(dir);
        IEnumerable<string> newFiles = Directory.EnumerateFiles(dir);

        foreach (string filePath in newFiles)
        {
            Console.WriteLine($"Found file {filePath}");
            files.Add(new FileInfo(filePath));
        }

        foreach (string subdir in subdirs)
        {
            Console.WriteLine($"Found dir {subdir}");
            dirs.Add(new DirectoryInfo(subdir));
            DiscoverFilesImpl(subdir, files, dirs);
        }

        return files;
    }
}
