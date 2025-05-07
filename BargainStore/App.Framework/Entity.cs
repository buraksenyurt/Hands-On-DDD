namespace App.Framework;

public abstract class Entity
{
    private readonly List<object> _events;
    protected abstract void When(object @event);
    protected abstract void ValidateSate();
    protected Entity()
    {
        _events = [];
    }
    protected void Raise(object @event)
    {
        When(@event);
        ValidateSate();
        _events.Add(@event);
    }
    public IEnumerable<object> GetChanges()
    {
        return _events.AsEnumerable();
    }
    public void ClearChanges()
    {
        _events.Clear();
    }
}
