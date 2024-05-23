
namespace Cooking.Domain.Abstractions;

public class Entity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; set; }

    protected Entity() { }

    protected Entity(Guid id)
    {
        Id = id;
    }
     
    public void ClearDomainEvents()
    {
       _domainEvents.Clear();
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
