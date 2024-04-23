using HSM.Contract.Abstractions.Message;
using HSM.Domain.Abstractions.Entities;

namespace HSM.Domain.Abstractions.Aggregates
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
            _domainEvents.Add(domainEvent);
    }
}
