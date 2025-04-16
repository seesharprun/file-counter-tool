namespace SeeSharpRun.FileCounter.Services;

/// <inheritdoc />
/// <remarks>
/// This implementation uses C# glob patterns to match files and directories. For more infromation, see <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/file-globbing"/>.
/// </remarks>
public sealed class GlobFileIndexerService(
    ILogger<GlobFileIndexerService> logger,
    Settings settings
) : IFileIndexerService
{
    /// <inheritdoc />
    public async Task<Dictionary<string, int>> IndexFilesAsync(CancellationToken cancellationToken)
    {
        logger.LogIndexingFiles();

        string root = settings.Root.FullName;

        Matcher matcher = new();
        matcher.AddIncludePatterns([
            "**/*",
        ]);
        matcher.AddExcludePatterns([
            ".git/**/*"
        ]);
        IEnumerable<string> files = matcher.GetResultsInFullPath(root);

        ConcurrentDictionary<string, int> fileLineCount = [];
        await Parallel.ForEachAsync(files, cancellationToken, async (file, cancellationToken) =>
        {
            int lineCount = (await File.ReadAllLinesAsync(file, cancellationToken)).Length;
            string relativeFile = Path.GetRelativePath(root, file);
            bool _ = fileLineCount.TryAdd(relativeFile, lineCount);
        });

        return fileLineCount.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
    }
}

internal static partial class Logging
{
    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Information,
        Message = "Indexing files...")]
    public static partial void LogIndexingFiles(this ILogger logger);
}