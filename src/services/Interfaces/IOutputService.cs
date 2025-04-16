namespace SeeSharpRun.FileCounter.Services.Interfaces;

/// <summary>
/// Services that renders the output of the file indexer.
/// </summary>
public interface IOutputService
{
    /// <summary>
    /// Renders the output of the file indexer.
    /// </summary>
    /// <param name="input">
    /// A dictionary containing the relative file paths and their corresponding line counts.
    /// </param>
    void RenderOutput(Dictionary<string, int> input);
}