using AStar.FilesApi.Models;

namespace AStar.FilesApi.Files;

/// <summary>
/// The <see href="DuplicateGroup"></see> class.
/// </summary>
public class DuplicateGroup
{
    /// <summary>
    /// Gets or sets the Id of the File Group.
    /// </summary>
    public FileSizeDto Group { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of <see href="FileInfoDto"></see>.
    /// </summary>
    public IReadOnlyCollection<FileInfoDto> Files { get; set; } = [];
}
