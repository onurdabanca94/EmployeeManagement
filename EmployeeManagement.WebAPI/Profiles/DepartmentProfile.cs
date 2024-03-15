using AutoMapper;
using EmployeeManagement.DataAccess.Domain;
using EmployeeManagement.WebAPI.Dtos.Department;

namespace EmployeeManagement.WebAPI.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();
        }
    }
}
