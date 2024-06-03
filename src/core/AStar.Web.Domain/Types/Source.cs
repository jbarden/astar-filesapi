namespace AStar.Web.Domain.Types;

public class Source
{
    private string? value;

    public string? Value
    {
        get => value;
        set => this.value = value?.Trim();
    }

    public static implicit operator Source(string? value) => new() { Value = value };

    public static implicit operator string?(Source value) => value.Value;
}
