# MarkdownTOC
MarkdownTOC will create or update your markdown file's Table of Contents. It is a simply tool that will iterate your headings defined in your markdown file and create a basic Table of Contents with hyperlinks to navigate within your markdown file.

Usage of the tool is pretty simple -- simply build the console application and run it from the command line:

```sh
MarkDownTOC.exe toc -f C:\Folder\README.md
```

The tool uses the CommandLine NuGet package to handle command line arguments. This gives nice Unix-like built in man pages for how to use the tool. Here is an example to list the various verbs available:

```sh
MarkDownTOC.exe help
```

To get help with a specific verb:

```sh
MarkDownTOC.exe help toc
```

