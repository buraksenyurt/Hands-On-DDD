namespace App.Domain;

public record BookCrateDate(DateTime Value)
    : CreateDate(Value)
{
    public static BookCrateDate FromString(string value)
    {
        Validate(value);
        return new BookCrateDate(DateTime.Parse(value));
    }

    private static void Validate(string value)
    {
        if (value == default)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Book crate date can not be default");
        }
        if (!DateTime.TryParse(value, out DateTime _))
        {
            throw new ArgumentException("Invalid date");
        }
    }
}
