using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using AStar.FilesApi.Config;

namespace AStar.FilesApi.Files;

public class CountSearchParameters
{
    [Required]
    public string SearchFolder { get; set; } = string.Empty;

    public bool Recursive { get; set; } = true;

    public bool ExcludeViewed { get; set; }

    public bool IncludeSoftDeleted { get; set; }

    public bool IncludeMarkedForDeletion { get; set; }

    public string? SearchText { get; set; }

    public SearchType SearchType { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
