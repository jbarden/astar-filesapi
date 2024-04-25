using AStar.FilesApi.Domain.Types;

namespace AStar.FilesApi.Domain.Functions;

public static class FileNameAndPathExtensions
{
    public static bool FileAlreadyDownloaded(this IEnumerable<FileNameAndPath> files, FileName fileName) => files.ToList().Contains(fileName.Value);
}
