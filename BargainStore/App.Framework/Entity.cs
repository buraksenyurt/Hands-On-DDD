namespace App.Framework;

public abstract class Entity<TId>(Action<object> applier) : IDomainEventHandler
    where TId : ValueObject<TId>
{
    private readonly Action<object> _applier = applier;

    public TId Id { get; protected set; }

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
