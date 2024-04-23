using HSM.Domain.Abstractions.Aggregates;
using HSM.Domain.Abstractions.Entities;
using System.Diagnostics;

namespace HSM.Domain.Entities
{
    public class Department : AggregateRoot<Guid>, IAuditableEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset CreatedOnUtc { get; set; }
        public DateTimeOffset? ModifiedOnUtc { get; set; }

        public Department(Guid id, string name, string description)
        {
            //if (name.Length > 10)
            //    throw new ProductFieldException(nameof(Name));

            Id = id;
            Name = name;
            Description = description;
        }


        public static Department CreateDepartment(Guid id, string name, string description)
        {
            var department = new Department(id,  name, description);
            department.RaiseDomainEvent(new Contract.Services.V1.Department.DomainEvent.DepartmentCreated(
                Guid.NewGuid(), 
                department.Id, 
                department.Name,
                department.Description));
            return department;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
            RaiseDomainEvent(new Contract.Services.V1.Department.DomainEvent.DepartmentUpdated(Guid.NewGuid(), Id, name, description));
        }

        public void Delete()
            => RaiseDomainEvent(new Contract.Services.V1.Department.DomainEvent.DepartmentDeleted(Guid.NewGuid(), Id));
    }
}
