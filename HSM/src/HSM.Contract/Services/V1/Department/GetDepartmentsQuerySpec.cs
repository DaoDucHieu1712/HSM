using HSM.Application.Common.Models;
using HSM.Application.Dto;
using HSM.Application.Params;
using MediatR;

namespace HSM.Contract.Services.V1.Department
{
    public class GetDepartmentsQuerySpec : SearchDepartmentParam,
    IRequest<ResponseBase<PaginationResponse<DepartmentDto>>>;
}
