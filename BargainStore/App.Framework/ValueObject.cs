namespace App.Framework;

public abstract record ValueObject<T>
    where T : ValueObject<T>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();
}

