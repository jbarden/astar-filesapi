using AStar.FilesApi.Domain.Types;

namespace AStar.FilesApi.Domain.Functions;

public static class DirectoryNameExtensions
{
    public static bool HasValue(this DirectoryName tagToUse) => !tagToUse.HasNoValue();

    public static bool HasNoValue(this DirectoryName tagToUse) => string.IsNullOrWhiteSpace(tagToUse.Value);
}
