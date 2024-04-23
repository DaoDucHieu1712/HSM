using MediatR;

namespace HSM.Contract.Abstractions.Message
{
    public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
    {
    }
}
