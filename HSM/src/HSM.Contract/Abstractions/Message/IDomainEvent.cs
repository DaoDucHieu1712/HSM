using MediatR;

namespace HSM.Contract.Abstractions.Message;

public interface IDomainEvent : INotification
{
    public Guid IdEvent { get; init; }
}