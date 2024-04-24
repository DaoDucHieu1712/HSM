using HSM.Application.Common.Models;
using HSM.Application.Dto;
using HSM.Application.Params;
using HSM.Contract.Abstractions.Message;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HSM.Contract.Services.V1.Department.Response;

namespace HSM.Contract.Services.V1.Department
{
    public static class Query
    {
        public class GetDepartmentsQuerySpec : SearchDepartmentParam,
            IRequest<ResponseBase<PaginationResponse<DepartmentDto>>>;

        public record GetDepartmentsQuery() : IQuery<List<DepartmentResponse>>;
        public record GetDepartmentByIdQuery(Guid Id) : IQuery<DepartmentResponse>;
    }
}
