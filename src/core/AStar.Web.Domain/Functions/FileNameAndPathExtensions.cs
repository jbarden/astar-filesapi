using AStar.Web.Domain.Types;

namespace AStar.Web.Domain.Functions;

public static class FileNameAndPathExtensions
{
    public static bool FileAlreadyDownloaded(this IEnumerable<FileNameAndPath> files, FileName fileName) => files.ToList().Contains(fileName.Value);
}
