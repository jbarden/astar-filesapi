using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using AStar.FilesApi.Config;
using AStar.Web.Domain;

namespace AStar.FilesApi.Files;

public class ListSearchParameters
{
    [Required]
    public string SearchFolder { get; set; } = string.Empty;

    public bool Recursive { get; set; } = true;

    public bool ExcludeViewed { get; set; }

    public bool IncludeSoftDeleted { get; set; }

    public bool IncludeMarkedForDeletion { get; set; }

    public string? SearchText { get; set; }

    [Required]
    public int CurrentPage { get; set; } = 1;

    [Required]
    public int ItemsPerPage { get; set; } = 10;

    [Range(50, 850, ErrorMessage = "Please specify a thumbnail size between 50 and 850 pixels.")]
    public int MaximumSizeOfThumbnail { get; set; } = 150;

    [Range(50, 999999, ErrorMessage = "Please specify an image size between 500 and 999999 (NOT recommended!) pixels.")]
    public int MaximumSizeOfImage { get; set; } = 1500;

    [Required]
    public SortOrder SortOrder { get; set; }

    public SearchType SearchType { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
