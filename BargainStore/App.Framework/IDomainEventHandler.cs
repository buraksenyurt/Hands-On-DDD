namespace App.Framework;

public interface IDomainEventHandler
{
    void Handle(object @event);
}
