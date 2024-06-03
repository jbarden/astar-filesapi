using System.Text.Json;
using AStar.Web.Domain;

namespace AStar.FilesApi.Models;

public class FileInfoDto
{
    public FileInfoDto(FileDetail fileDetail)
    {
        Name = fileDetail.FileName;
        FullName = Path.Combine(fileDetail.DirectoryName, fileDetail.FileName);
        Height = fileDetail.Height;
        Width = fileDetail.Width;
        Size = fileDetail.FileSize;
        DetailsLastUpdated = fileDetail.DetailsLastUpdated;
        LastViewed = fileDetail.LastViewed;
        SoftDeleted = fileDetail.SoftDeleted;
        SoftDeletePending = fileDetail.SoftDeletePending;
        NeedsToMove = fileDetail.NeedsToMove;
        HardDeletePending = fileDetail.HardDeletePending;
    }

    public FileInfoDto()
    {
    }

    public string Name { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public long Height { get; set; }

    public long Width { get; set; }

    public long Size { get; set; }

    /// <summary>
    /// Gets or sets the date the file details were last updated. I know, shocking...
    /// </summary>
    public DateTime? DetailsLastUpdated { get; set; }

    /// <summary>
    /// Gets or sets the date the file wase last viewed. I know, shocking...
    /// </summary>
    public DateTime? LastViewed { get; set; }

    public string Extension
    {
        get
        {
            var extensionIndex = Name.LastIndexOf('.') + 1;
            return Name[extensionIndex..];
        }
    }

    /// <summary>
    /// Gets or sets whether the file has been 'soft deleted'. I know, shocking...
    /// </summary>
    public bool SoftDeleted { get; set; }

    /// <summary>
    /// Gets or sets whether the file has been marked as 'delete pending'. I know, shocking...
    /// </summary>
    public bool SoftDeletePending { get; set; }

    /// <summary>
    /// Gets or sets whether the NeedsToMove flag is set for the file
    /// </summary>
    public bool NeedsToMove { get; set; }

    /// <summary>
    /// Gets or sets whether the HardDeletePending flag is set for the file
    /// </summary>
    public bool HardDeletePending { get; set; }

    /// <summary>
    /// Returns this object in JSON format.
    /// </summary>
    /// <returns>This object serialized as a JSON object.</returns>
    public override string ToString() => JsonSerializer.Serialize(this);
}
