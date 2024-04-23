using AutoMapper;
using HSM.Contract.Services.V1.Department;
using HSM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Mapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() {
            DepartmentMapper();
        }

        public void DepartmentMapper()
        {
            // V1
            CreateMap<Department, Response.DepartmentResponse>().ReverseMap();
        }
    }
}
