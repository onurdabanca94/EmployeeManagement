using AutoMapper;
using EmployeeManagement.DataAccess.Domain;
using EmployeeManagement.WebAPI.Dtos.User;

namespace EmployeeManagement.WebAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}
