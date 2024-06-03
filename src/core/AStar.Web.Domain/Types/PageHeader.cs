namespace AStar.Web.Domain.Types;

public record PageHeader
{
    private string? value;

    public string? Value
    {
        get => value;
        set => this.value = value?.Trim();
    }

    public static implicit operator PageHeader(string? value) => new() { Value = value };

    public static implicit operator string?(PageHeader value) => value.Value;
}
