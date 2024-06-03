using System.Text.Json.Serialization;

namespace AStar.Web.Domain;

[JsonConverter(typeof(JsonStringEnumConverter<SortOrder>))]
public enum SortOrder
{
    SizeDescending,
    SizeAscending,
    NameDescending,
    NameAscending
}
