namespace SiteGenerator;

internal class Program
{
    public static void Main(string content, string? output)
    {
        Console.WriteLine("Hello, World!");
        ValidatePathArg(content, nameof(content));
    }

    private static void ValidatePathArg(string path, string argName)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"The {argName} directory '{path}' does not exist.");
        }
    }
}
