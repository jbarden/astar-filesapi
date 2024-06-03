using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace AStar.FilesApi.Endpoints.Files;

public abstract class BaseSearchParameters
{
    [Required]
    public string SearchFolder { get; set; } = string.Empty;

    public bool Recursive { get; set; } = true;

    public bool IncludeSoftDeleted { get; set; }

    public bool IncludeMarkedForDeletion { get; set; }

    public string? SearchText { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
