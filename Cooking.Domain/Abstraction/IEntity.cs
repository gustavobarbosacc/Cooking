namespace Cooking.Domain.Abstraction;

public interface IEntity 
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}
