using AStar.Web.Domain.Types;

namespace AStar.Web.Domain.Functions;

public static class FilePrefixExtensions
{
    public static bool HasPrefixAlready(this FilePrefix filePrefix, TagToUse tagToUse) => filePrefix.HasValue() && filePrefix.Value!.Contains(tagToUse.Value!);

    public static bool DoesNotHavePrefixAlready(this FilePrefix filePrefix, TagToUse tagToUse) => !filePrefix.HasPrefixAlready(tagToUse);

    public static string AppendFilePrefix(this FilePrefix filePrefix, TagToUse tagToUse) => string.Join('-', filePrefix, tagToUse);

    public static bool HasValue(this FilePrefix tagToUse) => !tagToUse.HasNoValue();

    public static bool HasNoValue(this FilePrefix tagToUse) => !string.IsNullOrWhiteSpace(tagToUse.Value);
}
