namespace AStar.Web.Domain.Types;

public record DirectoryName
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

            this.value = value.Trim().Replace("/", "AKA").Replace("\"", "'");
        }
    }

    public static implicit operator DirectoryName(string? value) => new() { Value = value };

    public static implicit operator string?(DirectoryName value) => value.Value;
}
