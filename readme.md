# `filecounter` command-line tool

This tool counts the number of files in a directory and generates a table with output for the number of files and number of lines per file.

> [!IMPORTANT]
> This tool ignores the `.git` directory by default.

## Install

```shell
dotnet tool install --global cosmicworks
```

## Use

```shell
filecounter --path "<path-to-target-directory>"
```

## Arguments

| | Description |
| :---: | :--- |
| **`--path`** | The path to the directory to search for files |
