namespace AStar.Web.Domain.Types;

public record FileNameAndPath
{
    private string? value;

    public string? Value
    {
        get => value;
        set => this.value = value?.Trim();
    }

    public static implicit operator FileNameAndPath(string? value) => new() { Value = value };

    public static implicit operator string?(FileNameAndPath value) => value.Value;
}
