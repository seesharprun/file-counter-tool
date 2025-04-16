namespace SeeSharpRun.FileCounter.Services.Interfaces;

/// <summary>
/// Service that indexes files in a directory and counts the number of lines in each file.
/// </summary>
public interface IFileIndexerService
{
    /// <summary>
    /// Indexes files in a directory and counts the number of lines in each file.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token to cancel the operation.
    /// </param>
    /// <returns>
    /// A dictionary containing the relative file paths and their corresponding line counts.
    /// </returns>
    Task<Dictionary<string, int>> IndexFilesAsync(CancellationToken cancellationToken);
}