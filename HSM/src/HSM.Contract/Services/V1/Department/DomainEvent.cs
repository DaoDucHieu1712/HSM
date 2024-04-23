using HSM.Contract.Abstractions.Message;

namespace HSM.Contract.Services.V1.Department
{
    public static class DomainEvent
    {
        public record DepartmentCreated(Guid IdEvent, Guid Id , string Name, string Description) : IDomainEvent;
        public record DepartmentUpdated(Guid IdEvent, Guid Id , string Name, string Description) : IDomainEvent;
        public record DepartmentDeleted(Guid IdEvent, Guid Id) : IDomainEvent;
    }
}
