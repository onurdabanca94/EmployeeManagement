﻿namespace EmployeeManagement.WebAPI.Dtos.User
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
