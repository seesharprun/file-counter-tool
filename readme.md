# File Counter

A .NET command-line tool for counting files and lines of code in a directory. This tool recursively scans directories, counts all files, and calculates the total number of lines in each file, presenting the results in a formatted table.

> [!TIP]
> The `.git` directory is automatically excluded from scanning.

## Arguments

| Short | Long | Description | Default |
| --- | --- | --- | --- |
| | --path | Directory to scan for files (required) | |
| -h | --help | Renders help metadata | |

## Prerequisites

- [.NET 10](https://dotnet.microsoft.com/download/dotnet/10.0)

## Dynamically run package

Run the tool directly without installation using `dnx`:

```shell
dnx SeeSharpRun.FileCounter --path "<directory>"
```

## Install package

Install the tool globally on your machine:

```shell
dotnet tool install --global SeeSharpRun.FileCounter
```

After installation, run using `dotnet tool run`:

```shell
dotnet tool run SeeSharpRun.FileCounter --path "<directory>"
```

Or use the shorter command alias:

```shell
filecounter --path "<directory>"
```

## Run in dev

For development and testing, run directly from source:

```shell
dotnet run --project src/tool/SeeSharpRun.FileCounter.Tool.csproj -- --path "<directory>"
```
