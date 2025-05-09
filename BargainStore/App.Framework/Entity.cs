namespace App.Framework;

public abstract class Entity<TId> : IDomainEventHandler
    where TId : ValueObject<TId>
{
    private readonly Action<object> _applier;

    public TId Id { get; protected set; }

    protected Entity(Action<object> applier) => _applier = applier;

    protected abstract void When(object @event);

    protected void Raise(object @event)
    {
        When(@event);
        _applier(@event);
    }

    void IDomainEventHandler.Handle(object @event)
    {
        When(@event);
    }
}
