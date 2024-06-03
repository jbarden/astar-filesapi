using AStar.Web.Domain.Types;

namespace AStar.Web.Domain.Functions;

public static class HRefExtensions
{
    public static bool IsRequiredHRef(this HRef hRef) => !string.IsNullOrWhiteSpace(hRef.Value) && hRef.Value.Contains("/w/");

    public static string AsFileName(this HRef hRef)
    {
        var indexOfFinalSlash = hRef.HRefIndex();

        return !string.IsNullOrWhiteSpace(hRef.Value) && indexOfFinalSlash > 0
            ? hRef.Value[indexOfFinalSlash..]
            : string.Empty;
    }

    public static IEnumerable<HRef> FilterFilesAlreadyDownloaded(this IEnumerable<HRef> hRefs,
        IEnumerable<FileNameAndPath> existingFiles)
        => hRefs.ExceptBy<HRef, FileNameAndPath>(existingFiles, h => h.Value);

    public static bool HasValue(this HRef hRef) => !string.IsNullOrWhiteSpace(hRef.Value);

    public static bool HasNoValue(this HRef hRef) => !hRef.HasValue();

    private static int HRefIndex(this HRef hRef)
        => hRef.HasValue()
            ? hRef.Value!.IndexOf("/", StringComparison.OrdinalIgnoreCase) + 1
            : 0;
}
