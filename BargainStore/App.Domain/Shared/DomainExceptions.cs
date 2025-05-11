namespace App.Domain.Shared;

public static class DomainExceptions
{
    public class InvalidEntityStateException(object entity, string message)
        : Exception($"Entity {entity.GetType().Name} state change rejected,{message}")
    {
    }
    public class IllegalWordsFoundException(string text)
        : Exception($"Illegal words found in {text}")
    {
    }
}
