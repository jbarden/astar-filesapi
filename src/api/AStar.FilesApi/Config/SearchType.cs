using System.Text.Json.Serialization;

namespace AStar.FilesApi.Config;

[JsonConverter(typeof(JsonStringEnumConverter<SearchType>))]
public enum SearchType
{
    Images,
    All,
    Duplicates
}
