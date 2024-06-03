using AStar.Web.Domain.Types;

namespace AStar.Web.Domain.Functions;

public static class TagToUseExtensions
{
    public static bool HasValue(this TagToUse tagToUse) => !tagToUse.HasNoValue();

    public static bool HasNoValue(this TagToUse tagToUse) => string.IsNullOrWhiteSpace(tagToUse.Value);

    public static bool IsNotRequiredTag(this TagToUse tagToUse, string[] tagsToCompare)
        => tagToUse.HasValue()
        && tagsToCompare.Contains(tagToUse.Value, StringComparer.OrdinalIgnoreCase);

    public static bool IsPeopleTag(this TagToUse tagToUse, string[] tagsToIgnore)
        => tagToUse.HasValue()
        && tagToUse.IsRequiredTag(tagsToIgnore)
        && !tagToUse.Value!.StartsWith("model", StringComparison.CurrentCultureIgnoreCase)
        && (ContainsText(tagToUse.Value!, "people > model") || ContainsText(tagToUse.Value!, "people > por"));

    public static bool IsCelebTag(this TagToUse tagToUse, string[] tagsToIgnore)
        => tagToUse.HasValue()
        && !tagToUse.IsNotRequiredTag(tagsToIgnore)
        && (ContainsText(tagToUse.Value!, "actress") || ContainsText(tagToUse.Value!, "celeb"));

    public static bool IsVehicleTag(this TagToUse tagToUse, string[] tagsToIgnore)
        => tagToUse.HasValue()
        && !tagToUse.IsNotRequiredTag(tagsToIgnore)
        && !tagToUse.IsVehicleTagToIgnore()
        && ContainsText(tagToUse.Value!, "Vehicles > Cars & Motorcycles");

    public static bool IsVehicleTagToIgnore(this TagToUse tagToUse)
        => tagToUse.HasValue()
        && (tagToUse.Value!.Equals("car", StringComparison.CurrentCultureIgnoreCase)
            || tagToUse.Value!.Contains("cars", StringComparison.CurrentCultureIgnoreCase));

    private static bool ContainsText(TagToUse tagToUse, string comparisonText) => tagToUse.Value!.Contains(comparisonText, StringComparison.CurrentCultureIgnoreCase);

    private static bool IsRequiredTag(this TagToUse tagToUse, IEnumerable<string> tagsToCompare) => tagToUse.HasValue() && tagsToCompare.Contains(tagToUse.Value, StringComparer.OrdinalIgnoreCase);
}
