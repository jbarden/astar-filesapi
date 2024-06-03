namespace AStar.Web.Domain.Types;

public record FilePrefix
{
    private string? value;

    public string? Value
    {
        get => value;
        set
        {
            value ??= string.Empty;

            if(value.StartsWith('-'))
            {
                value = value[1..];
            }

            this.value = value.Trim().Replace("/", "AKA");
        }
    }

    public static implicit operator FilePrefix(string? value) => new() { Value = value };

    public static implicit operator string?(FilePrefix value) => value.Value;
}
