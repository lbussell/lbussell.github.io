namespace SiteGenerator;

internal class SiteGeneratorPipeline : FileBasedPipeline<string, BlogContext>
{
    public SiteGeneratorPipeline(BlogContext initialContext)
        : base(initialContext)
    {
    }

    public static SiteGeneratorPipeline Create(
        string contentDir,
        string outputDir,
        BlogContext? initialContext)
    {
        var context = initialContext ?? new BlogContext([]);

        return new SiteGeneratorPipeline(context)
        {
            ContentDir = contentDir,
            OutputDir = outputDir,
        };
    }

    protected override Task<string> ReadFileContentsAsync(FileInfo file) =>
        File.ReadAllTextAsync(file.Name);

    protected override Task WriteFileContentsAsync(string path, string content) =>
        File.WriteAllTextAsync(path, content);
}
