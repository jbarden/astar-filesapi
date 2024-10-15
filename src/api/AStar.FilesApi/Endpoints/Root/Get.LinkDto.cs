namespace AStar.FilesApi.Endpoints.Root;

public class LinkDto
{
    public string Rel { get; internal set; } = string.Empty;

    public string Href { get; internal set; } = string.Empty;

    public string Method { get; internal set; } = "GET";
}
