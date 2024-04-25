using System.Text;

namespace AStar.FilesApi.Domain;

public class FileDetail
{
    public string FullName => Path.Combine(DirectoryName, FileName);

    public bool IsImage => FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                           || FileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                           || FileName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase)
                           || FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                           || FileName.EndsWith(".jif", StringComparison.OrdinalIgnoreCase)
                           || FileName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase);

    public string FileName { get; set; } = string.Empty;

    public DateTime? DetailsLastUpdated { get; set; }

    public DateTime? LastViewed { get; set; }

    public string DirectoryName { get; set; } = string.Empty;

    public int Height { get; set; }

    public int Width { get; set; }

    public long FileSize { get; set; }

    public bool SoftDeleted { get; set; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        _ = stringBuilder.Append($"FileName={FileName}&");
        _ = stringBuilder.Append($"DirectoryName={DirectoryName}&");
        _ = stringBuilder.Append($"IsImage={IsImage}&");
        _ = stringBuilder.Append($"DetailsLastUpdated={DetailsLastUpdated}&");
        _ = stringBuilder.Append($"LastViewed={LastViewed}&");
        _ = stringBuilder.Append($"Height={Height}&");
        _ = stringBuilder.Append($"Width={Width}&");
        _ = stringBuilder.Append($"FileSize={FileSize}&");
        _ = stringBuilder.Append($"SoftDeleted={SoftDeleted}");

        return stringBuilder.ToString();
    }
}
