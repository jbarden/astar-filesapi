using AStar.Web.Domain.Types;

namespace AStar.Web.Domain.Functions;

public static class SubscriptionHeaderExtensions
{
    public static int PageCount(this PageHeader pageHeader)
    {
        if(string.IsNullOrWhiteSpace(pageHeader.Value))
        {
            return 0;
        }

        var firstSpaceIndex = pageHeader.Value.IndexOf(" ", StringComparison.OrdinalIgnoreCase);
        var searchResultCountRaw = pageHeader.Value.Replace(",", string.Empty)[..firstSpaceIndex];

        var x = decimal.Parse(searchResultCountRaw) / 24;
        return Convert.ToInt32(Math.Ceiling(x));
    }

    public static string HeaderText(this PageHeader pageHeader, string searchType)
    {
        if(string.IsNullOrWhiteSpace(pageHeader.Value))
        {
            return string.Empty;
        }

        var headerIndex = pageHeader.HeaderIndex(searchType);

        return pageHeader.Value[headerIndex..].Replace(' ', '-').Replace("#", string.Empty)
            .Replace("Wallpapers-found--for-", string.Empty);
    }

    public static bool HasValue(this PageHeader pageHeader) => !string.IsNullOrWhiteSpace(pageHeader.Value);

    public static bool HasNoValue(this PageHeader pageHeader) => !pageHeader.HasValue();

    private static int HeaderIndex(this PageHeader header, string searchType)
        => string.IsNullOrWhiteSpace(header.Value)
            ? 0
            : SetSearchType(header, searchType);

    private static int SetSearchType(PageHeader header, string searchType)
        => searchType == "Search"
            ? header.Value!.IndexOf("Wallpapers found  for", StringComparison.OrdinalIgnoreCase)
            : header.Value!.IndexOf("New", StringComparison.OrdinalIgnoreCase);
}
