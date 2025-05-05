namespace App.Domain;

public record BookDetails
{
    public string Value { get; }

    internal BookDetails(string value)
    {
        Value = value;
    }

    public static BookDetails FromString(string value)
    {
        return new BookDetails(value);
    }

    public static implicit operator string(BookDetails details)
    {
        return details.Value;
    }
}
