namespace App.Domain.Exceptions;

public class InvalidEntityStateException(object entity, string message)
    : Exception($"Entity {entity.GetType().Name} state change rejected,{message}")
{
}
