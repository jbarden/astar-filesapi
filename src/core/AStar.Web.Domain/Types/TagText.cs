namespace AStar.Web.Domain.Types;

public record TagText
{
    private string? value;

    public string? Value
    {
        get => value;
        set => this.value = value?.Trim();
    }

    public static implicit operator TagText(string? value) => new() { Value = value };

    public static implicit operator string?(TagText value) => value.Value;
}
