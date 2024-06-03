using System.ComponentModel.DataAnnotations;

namespace AStar.FilesApi.Endpoints.Files;

public class DirectoryChangeRequest
{
    [Required]
    public string OldDirectoryName { get; set; } = string.Empty;

    [Required]
    public string NewDirectoryName { get; set; } = string.Empty;

    [Required]
    public string FileName { get; set; } = string.Empty;
}
