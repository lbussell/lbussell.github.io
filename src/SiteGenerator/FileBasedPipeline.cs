using System.CommandLine.Rendering;

namespace SiteGenerator;

internal abstract class FileBasedPipeline<T, TContext> : IPipeline<T, TContext>
{
    private readonly List<IProcessingStep<T, TContext>> _steps = [];

    private TContext _context;

    protected FileBasedPipeline(TContext initialContext)
    {
        _context = initialContext;
    }

    public required string ContentDir { get; init; }

    public required string OutputDir { get; init; }

    protected TContext Context => _context;

    public IPipeline<T, TContext> Add(IProcessingStep<T, TContext> step)
    {
        _steps.Add(step);
        return this;
    }

    public async void GenerateAsync(T input)
    {
        (IReadOnlyList<FileInfo> files, IReadOnlyList<DirectoryInfo> dirs) =
            FilesHelper.DiscoverFiles(ContentDir);

        IEnumerable<FileInfo> outputFiles = files
            .Select(fileInfo => fileInfo.Name)
            .Select(path => path.Replace(ContentDir, OutputDir))
            .Select(path => new FileInfo(path));

        IEnumerable<DirectoryInfo> outputDirs = dirs
            .Select(dirInfo => dirInfo.Name)
            .Select(dir => dir.Replace(ContentDir, OutputDir))
            .Select(path => new DirectoryInfo(path));

        IEnumerable<Task<T>> fileContents = files.Select(ReadFileContentsAsync);

        foreach (IProcessingStep<T, TContext> step in _steps)
        {
            fileContents = fileContents
                .Select(async content =>
                    step.Process(await content, ref _context));

            foreach (Task<T> fileContent in fileContents)
            {
                step.Process(await fileContent, ref _context);
            }
        }

        IEnumerable<(Task<T> Contents, FileInfo FileInfo)> outputInfos =
            fileContents.Zip(outputFiles);

        foreach (DirectoryInfo outputDir in outputDirs)
        {
            outputDir.Create();
        }

        await Parallel.ForEachAsync(outputInfos, async (outputInfo, _) =>
            await WriteFileContentsAsync(outputInfo.FileInfo.Name, await outputInfo.Contents));
    }

    protected abstract Task<T> ReadFileContentsAsync(FileInfo file);

    protected abstract Task WriteFileContentsAsync(string path, T content);
}
