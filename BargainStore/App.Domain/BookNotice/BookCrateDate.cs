namespace App.Domain.BookNotice;

public record Date(DateTime Value)
    : CreateDate(Value)
{
    public static Date FromString(string value)
    {
        Validate(value);
        return new Date(DateTime.Parse(value));
    }

    private static void Validate(string value)
    {
        if (value == default)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Date can not be default");
        }
        if (!DateTime.TryParse(value, out DateTime _))
        {
            throw new ArgumentException("Invalid date");
        }
    }
}
