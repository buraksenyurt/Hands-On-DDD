using App.Framework;

namespace App.Domain.Exceptions;

public class InvalidEntityStateException(Entity entity, string message)
    : Exception($"Entity {entity.GetType().Name} state change rejected,{message}")
{
}
