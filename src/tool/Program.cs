HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Configure logging to suppress info messages
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddTransient<IFileIndexerService, GlobFileIndexerService>();
builder.Services.AddTransient<IOutputService, SpectreOutputService>();

builder.Services.AddHostedService<ApplicationWorker>();

Option<DirectoryInfo> pathOption = new(
    name: "--path",
    description: "The path to the directory to search for files."
)
{
    IsRequired = true,
    Arity = ArgumentArity.ExactlyOne
};

RootCommand command =
[
    pathOption
];
command.Description = "Tool that counts the number of files in a directory and generates a table with output for the number of files and number of lines per file.";
command.SetHandler(async (path) =>
{
    builder.Services.AddSingleton(new Settings()
    {
        Root = path
    });

    IHost host = builder.Build();

    await host.RunAsync();
}, pathOption);

return await command.InvokeAsync(args);