namespace SeeSharpRun.FileCounter.Tool.Workers;

internal sealed class ApplicationWorker(
    ILogger<ApplicationWorker> logger,
    IHostApplicationLifetime applicationLifetime,
    IOutputService outputService,
    IFileIndexerService fileIndexerService
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogWorkerRunning(DateTimeOffset.Now);

        Dictionary<string, int> fileCounts = await fileIndexerService.IndexFilesAsync(cancellationToken);

        outputService.RenderOutput(fileCounts);

        applicationLifetime.StopApplication();
    }
}

internal static partial class Logging
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "Worker running at: {Time:R}")]
    public static partial void LogWorkerRunning(this ILogger logger, DateTimeOffset time);
}