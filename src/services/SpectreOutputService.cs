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
    public void RenderOutput(string targetDirectory, Dictionary<string, int> input)
    {
        ArgumentNullException.ThrowIfNull(input);

        logger.LogGeneratingOutput();

        int filesCount = input.Keys.Distinct().Count();
        int linesCount = input.Values.Sum();

        // Add a header label
        AnsiConsole.MarkupLine($"[cyan]Scanning directory:\t[bold]{targetDirectory}[/][/]");

        // Create a colorful table
        Table table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .AddColumn(new TableColumn("[cyan]File[/]").LeftAligned())
            .AddColumn(new TableColumn("[green]Lines[/]").RightAligned());

        foreach ((string file, int count) in input)
        {
            table.AddRow(
                $"[white]{file}[/]",
                $"[yellow]{count:N0}[/]"
            );
        }
        AnsiConsole.Write(table);

        // Create a colorful totals panel
        Grid grid = new Grid()
            .AddColumn()
            .AddRow($"[cyan]Total files:[/] [bold yellow]{filesCount:N0}[/]")
            .AddRow($"[cyan]Total lines:[/] [bold yellow]{linesCount:N0}[/]");

        Panel panel = new Panel(grid)
            .Header("[bold green]Totals[/]")
            .BorderColor(Color.Green)
            .Border(BoxBorder.Rounded);

        AnsiConsole.Write(panel);
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
