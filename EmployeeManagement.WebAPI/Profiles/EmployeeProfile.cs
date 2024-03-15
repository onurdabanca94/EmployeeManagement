using AutoMapper;
using EmployeeManagement.DataAccess.Domain;
using EmployeeManagement.WebAPI.Dtos.Employee;

namespace EmployeeManagement.WebAPI.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();

        }
    }
}
