using AutoMapper;
using EmployeeManagement.Business.Abstract;
using EmployeeManagement.DataAccess.Domain;
using EmployeeManagement.WebAPI.Dtos.Employee;
using EmployeeManagement.WebAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IMapper mapper, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetEmployeeWithDepartmentDto>> Get()
        {
            try
            {
                var list = await _employeeService.GetAllAsync().ConfigureAwait(false);
                if (list.HasValue())
                {
                    var response = (from e in list.ToList()
                                    join d in await _departmentService.GetAllAsync().ConfigureAwait(false)
                                    on e.DepartmentId equals d.Id
                                    select new GetEmployeeWithDepartmentDto()
                                    {
                                        Id = e.Id,
                                        DepartmentId = e.DepartmentId,
                                        Name = e.Name,
                                        DepartmentName = d.Name
                                    }).ToList();
                    return response;
                }
                return new List<GetEmployeeWithDepartmentDto>();
            }
            catch (Exception ex)
            {
                return new List<GetEmployeeWithDepartmentDto>();
            }
        }

        [HttpPost("get-all-employee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var list = await _employeeService.GetAllAsync().ConfigureAwait(false);
                if (list.HasValue())
                {
                    var response = list.Join(await _departmentService.GetAllAsync().ConfigureAwait(false), e => e.DepartmentId, d => d.Id, (x, y) =>
                        new GetEmployeeWithDepartmentDto()
                        {
                            Id = x.Id,
                            DepartmentId = x.DepartmentId,
                            Name = x.Name,
                            DepartmentName = y.Name
                        }).ToList();
                    return Ok(new { IsSuccess = true, data = response });
                }
                return NotFound(new { IsSuccess = false, message = "Çalışan bulunamadı." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("get-employee")]
        public async Task<IActionResult> GetEmployeeById([FromBody]GetEmployeeByIdDto getEmployeeByIdDto)
        {
            try
            {
                var data = await _employeeService.GetByIdAsync(getEmployeeByIdDto.Id).ConfigureAwait(false);
                if (data != null)
                {
                    var department = await _departmentService.GetByIdAsync(data.DepartmentId).ConfigureAwait(false);
                    if (department == null)
                    {
                        return NotFound(new { IsSuccess = false, message = "Departman bulunamadı." });
                    }
                    var response = new GetEmployeeWithDepartmentDto()
                    {
                        DepartmentId = data.DepartmentId,
                        Id = data.Id,
                        Name = data.Name,
                        DepartmentName = department.Name,
                    };
                    return Ok(new { IsSuccess = true, data = response });
                }
                return NotFound(new { IsSuccess = false, message = "Çalışan bulunamadı." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("get-employees-by-department-id")]
        public async Task<IActionResult> GetEmployeesByDepartmentId([FromBody]GetEmployeesByDepartmentIdDto getEmployeesByDepartmentIdDto)
        {
            try
            {
                var data = _employeeService.GetListByExpressionAsync(x => x.DepartmentId == getEmployeesByDepartmentIdDto.DepartmentId);
                if (data.HasValue())
                {
                    var response = data.Join(await _departmentService.GetAllAsync().ConfigureAwait(false), e => e.DepartmentId, d => d.Id, (x, y) =>
                        new GetEmployeeWithDepartmentDto()
                        {
                            Id = x.Id,
                            DepartmentId = x.DepartmentId,
                            Name = x.Name,
                            DepartmentName = y.Name
                        }).ToList();

                    return Ok(new { IsSuccess = true, data = response });
                }
                return NotFound(new { IsSuccess = false, message = "Çalışan bulunamadı." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("create-parfume")]
        public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                var department = await _departmentService.GetByIdAsync(createEmployeeDto.DepartmentId).ConfigureAwait(false);
                if (department == null)
                {
                    return NotFound(new { IsSuccess = false, message = "Departman bulunamadı." });
                }
                var mappedData = _mapper.Map<Employee>(createEmployeeDto);
                mappedData.DepartmentName = department;
                await _employeeService.AddAsync(mappedData).ConfigureAwait(false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("update-parfume")]
        public async Task<IActionResult> UpdateEmployee([FromBody]UpdateEmployeeDto updateEmployeeDto)
        {
            try
            {
                var foundData = await _employeeService.GetByIdAsync(updateEmployeeDto.Id).ConfigureAwait(false);
                if (foundData == null)
                {
                    return NotFound(new { IsSuccess = false, message = "Çalışan bulunamadı." });
                }
                foundData.Name = updateEmployeeDto.Name;
                await _employeeService.UpdateAsync(foundData).ConfigureAwait(false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("delete-employee")]
        public async Task<IActionResult> DeleteEmployee([FromBody]DeleteEmployeeDto deleteEmployeeDto)
        {
            try
            {
                var foundData = await _employeeService.GetByIdAsync(deleteEmployeeDto.Id).ConfigureAwait(false);
                if (foundData == null)
                {
                    return NotFound(new { IsSuccess = false, message = "Çalışan bulunamadı." });
                }
                await _employeeService.DeleteAsync(foundData).ConfigureAwait (false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }
    }
}
