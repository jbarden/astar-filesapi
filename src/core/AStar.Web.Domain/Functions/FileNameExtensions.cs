using AStar.Web.Domain.Types;

namespace AStar.Web.Domain.Functions;

public static class FileNameExtensions
{
    public static bool HasValue(this FileName tagToUse) => !tagToUse.HasNoValue();

    public static bool HasNoValue(this FileName tagToUse) => string.IsNullOrWhiteSpace(tagToUse.Value);
}
