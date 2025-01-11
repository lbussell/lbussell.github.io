namespace SiteGenerator;

internal class Program
{
    public static void Main(string content, string output)
    {
        ValidatePathArg(content, nameof(content));
        ValidatePathArg(output, nameof(output));

        // FilesHelper.DiscoverFiles(content);

        /*

        var pipeline = new SiteGeneratorPipeline()
            .Add(TagsProccessor)
            .Add(BlogProcessor)
            .Add(LiquidProcessor)
            .Add(MarkdownProcessor);

        string output = pipeline.Generate();

        */
    }

    private static void ValidatePathArg(string path, string argName)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException(
                $"The {argName} directory '{path}' does not exist.");
        }
    }
}
