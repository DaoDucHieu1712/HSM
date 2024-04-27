using AutoMapper;
using HSM.Application.Dto;
using HSM.Contract.Services.V1.Department;
using HSM.Domain.Entities;

namespace HSM.Application.Mapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            DepartmentMapper();
        }

        public void DepartmentMapper()
        {
            // V1
            CreateMap<Department, Response.DepartmentResponse>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
        }
    }
}
