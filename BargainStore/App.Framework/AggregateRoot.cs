namespace App.Framework;

public abstract class AggregateRoot<TId>
    : IDomainEventHandler
    where TId : ValueObject<TId>
{
    public TId Id { get; protected set; }
    private readonly List<object> _changes;
    protected AggregateRoot() => _changes = [];
    protected abstract void When(object @event);
    protected abstract void ValidateSate();

    protected void Raise(object @event)
    {
        When(@event);
        ValidateSate();
        _changes.Add(@event);
    }

    public IEnumerable<object> GetChanges()
    {
        return _changes.AsEnumerable();
    }

    public void ClearChanges()
    {
        _changes.Clear();
    }

    protected void ApplyToEntity(IDomainEventHandler entity, object @event)
    {
        entity?.Handle(@event);
    }

    void IDomainEventHandler.Handle(object @event)
    {
        When(@event);
    }
}
