using HSM.Application.Common.Interfaces;

namespace HSM.Application.Dto
{
    public class DepartmentDto : IDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
