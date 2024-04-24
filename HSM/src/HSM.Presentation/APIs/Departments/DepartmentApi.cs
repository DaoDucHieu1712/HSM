using Carter;
using HSM.Application.Params;
using HSM.Contract.Services.V1.Department;
using HSM.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using CommandV1 = HSM.Contract.Services.V1.Department;

namespace HSM.Presentation.APIs.Departments
{
    public class DepartmentApi : ApiEndpoint, ICarterModule
    {
        private const string BaseUrl = "/api/v{version:apiVersion}/departments";
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("departments")
                .MapGroup(BaseUrl).HasApiVersion(1);

            group1.MapPost(string.Empty , CreateDepartmentV1);
            group1.MapGet(string.Empty , GetDepartmentsV1);
            group1.MapPost(string.Empty, GetDepartmentsByConditionV1);
            group1.MapGet("{departmentId}" , GetDepartmentByIdV1);
            group1.MapDelete("{departmentId}",DeleteDepartmentV1);
            group1.MapPut("{departmentId}", UpdateDepartmentV1);
        }
        #region ====== version 1 ======

        public static async Task<IResult> CreateDepartmentV1(ISender sender, [FromBody] CommandV1.Command.CreateDepartmentCommand CreateDepartment)
        {
            var result = await sender.Send(CreateDepartment);

            if (result.IsFailure)
                return HandlerFailure(result);

            return Results.Ok(result);
        }

        public static async Task<IResult> GetDepartmentsV1(ISender sender)
        {
            var result = await sender.Send(new CommandV1.Query.GetDepartmentsQuery());
            return Results.Ok(result);
        }

        public static async Task<IResult> GetDepartmentsByConditionV1(ISender sender, CommandV1.Query.GetDepartmentsQuerySpec departmentsQuerySpec)
        {
            var result = await sender.Send(departmentsQuerySpec);
            return Results.Ok(result);
        }

        public static async Task<IResult> GetDepartmentByIdV1(ISender sender, Guid departmentId)
        {
            var result = await sender.Send(new CommandV1.Query.GetDepartmentByIdQuery(departmentId));
            return Results.Ok(result);
        }

        public static async Task<IResult> DeleteDepartmentV1(ISender sender, Guid departmentId)
        {
            var result = await sender.Send(new CommandV1.Command.DeleteDepartmentCommand(departmentId));
            return Results.Ok(result);
        }

        public static async Task<IResult> UpdateDepartmentV1(ISender sender, Guid departmentId, [FromBody] CommandV1.Command.UpdateDepartmentCommand UpdateDepartment)
        {
            var updateDepartmentCommand = new CommandV1.Command.UpdateDepartmentCommand(departmentId, UpdateDepartment.Name, UpdateDepartment.Description);
            var result = await sender.Send(updateDepartmentCommand);
            return Results.Ok(result);
        }

        #endregion ====== version 1 ======

    }
}
