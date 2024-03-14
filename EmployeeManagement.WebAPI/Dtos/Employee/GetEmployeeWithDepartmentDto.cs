namespace EmployeeManagement.WebAPI.Dtos.Employee
{
    public class GetEmployeeWithDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
