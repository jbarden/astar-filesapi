namespace AStar.Web.Domain.Types;

public record FileName
{
    private string? value;

    public string? Value
    {
        get => value;
        set => this.value = value?.Trim();
    }

    public static implicit operator FileName(string? value) => new() { Value = value };

    public static implicit operator string?(FileName value) => value.Value;
}
