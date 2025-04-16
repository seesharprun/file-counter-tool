namespace SeeSharpRun.FileCounter.Services;

/// <inheritdoc />
/// <remarks>
/// This implementation uses Spectre.Console to render the output in a table format. For more information, see <see href="https://spectreconsole.net/"/> 
/// </remarks>
public sealed class SpectreOutputService(
    ILogger<SpectreOutputService> logger
) : IOutputService
{
    /// <inheritdoc />
    public void RenderOutput(Dictionary<string, int> input)
    {
        ArgumentNullException.ThrowIfNull(input);

        logger.LogGeneratingOutput();

        int filesCount = input.Keys.Distinct().Count();
        int linesCount = input.Values.Sum();

        Table table = new Table()
            .AddColumns("File", "Lines");

        foreach ((string file, int count) in input)
        {
            table.AddRow($"{file}", $"{count:0000}");
        }
        Console.Write(table);

        List<string> totals = [
            $"Total files: {filesCount}",
            $"Total lines: {linesCount}"
        ];

        Rows rows = new(totals.Select(t => new Text(t)));
        Panel panel = new Panel(rows)
            .Header("Totals");
        Console.Write(panel);
    }
}

internal static partial class Logging
{
    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Information,
        Message = "Generating output...")]
    public static partial void LogGeneratingOutput(this ILogger logger);
}
