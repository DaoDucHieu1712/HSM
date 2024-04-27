using HSM.Contract.Abstractions.Message;
using static HSM.Contract.Services.V1.Department.Response;

namespace HSM.Contract.Services.V1.Department
{
    public static class Query
    {
        public record GetDepartmentsQuery() : IQuery<List<DepartmentResponse>>;
        public record GetDepartmentByIdQuery(Guid Id) : IQuery<DepartmentResponse>;
    }
}
