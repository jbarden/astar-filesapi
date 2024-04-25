namespace AStar.FilesApi.Models;

public class FileInfoDto
{
    public string Name { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public long Height { get; set; }

    public long Width { get; set; }

    public long Size { get; set; }

    public string Extension
    {
        get
        {
            var extensionIndex = Name.LastIndexOf(".", StringComparison.Ordinal) + 1;
            return Name[extensionIndex..];
        }
    }
}
