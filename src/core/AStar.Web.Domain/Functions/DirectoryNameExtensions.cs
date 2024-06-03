using AStar.Web.Domain.Types;

namespace AStar.Web.Domain.Functions;

public static class DirectoryNameExtensions
{
    public static bool HasValue(this DirectoryName tagToUse) => !tagToUse.HasNoValue();

    public static bool HasNoValue(this DirectoryName tagToUse) => string.IsNullOrWhiteSpace(tagToUse.Value);
}
