

namespace DistributedSystem.Contract.Abstractions.Message;

public interface IDomainEvent
{
    public Guid IdEvent { get; init; }
}