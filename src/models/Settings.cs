namespace SeeSharpRun.FileCounter.Models;

/// <summary>
/// Configuration for the file indexer tool.
/// </summary>
public sealed record Settings
{
    /// <summary>
    /// The root directory to search for files.
    /// </summary>
    public required DirectoryInfo Root { get; set; }
}