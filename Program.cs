using System;
using System.IO;
using System.IO.Compression;

namespace dotnet.tool;

internal class Program
{
    static void Main(string[] args)
    {
        //Unzip("./", "/Users/burnasheva/Downloads/archive.zip");
        Unzip("./", "/Users/burnasheva/Downloads/archive_202305.zip");
    }

    static void Unzip(string dest, string zipPath)
    {
        using (var archive = ZipFile.OpenRead(zipPath))
            // To fix : we need to filter out entries ending with `/` via "Where(e => !e.FullName.EndsWith("/", StringComparison.Ordinal))"
            foreach (var archiveEntry in archive.Entries)
            {
                var entryDestinationPath = Path.Combine(dest, archiveEntry.FullName);
                var canonicalEntryDestinationPath = Path.GetFullPath(entryDestinationPath);

                if (canonicalEntryDestinationPath.EndsWith(Path.DirectorySeparatorChar.ToString(),
                        StringComparison.Ordinal))
                    throw new Exception($"Invalid file path (ends with slash): '{canonicalEntryDestinationPath}'");
                archiveEntry.ExtractToFile(canonicalEntryDestinationPath);
            }
    }
}