using AutoMapper;
using EmployeeManagement.Business.Abstract;
using EmployeeManagement.DataAccess.Domain;
using EmployeeManagement.WebAPI.Dtos.Department;
using EmployeeManagement.WebAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpPost("get-all-department")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var list = await _departmentService.GetAllAsync().ConfigureAwait(false);
                if (list.HasValue())
                {
                    return Ok(new { IsSuccess = false, data = list.ToList() });
                }
                return NotFound(new { IsSuccess = false, message = "Departman bulunamadı."});
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("get-department-by-id")]
        public async Task<IActionResult> GetDepartmentById([FromBody]GetDepartmentByIdDto getDepartmentByIdDto)
        {
            try
            {
                var data = await _departmentService.GetByIdAsync(getDepartmentByIdDto.Id).ConfigureAwait(false);
                if (data != null)
                {
                    return Ok(new { IsSuccess = true, data = data });
                }
                return NotFound(new { IsSuccess = false, message = "Departman bulunamadı." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("add-department")]
        public async Task<IActionResult> AddDepartment([FromBody]CreateDepartmentDto createDepartmentDto)
        {
            try
            {
                var mappedData = _mapper.Map<Department>(createDepartmentDto);
                await _departmentService.AddAsync(mappedData).ConfigureAwait(false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("update-department")]
        public async Task<IActionResult> UpdateDepartment([FromBody]UpdateDepartmentDto updateDepartmentDto)
        {
            try
            {
                var foundData = await _departmentService.GetByIdAsync(updateDepartmentDto.Id).ConfigureAwait(false);
                if (foundData == null) 
                {
                    return NotFound(new { IsSuccess = false, message = "Departman bulunamadı." });
                }
                foundData.Name = updateDepartmentDto.Name;
                await _departmentService.UpdateAsync(foundData).ConfigureAwait(false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("delete-department")]
        public async Task<IActionResult> DeleteDepartment([FromBody]DeleteDepartmentDto deleteDepartmentDto)
        {
            try
            {
                var foundData = await _departmentService.GetByIdAsync(deleteDepartmentDto.Id).ConfigureAwait(false);
                if(foundData == null)
                {
                    return NotFound(new { IsSuccess = false, message = "Departman" });
                }
                await _departmentService.DeleteAsync(foundData).ConfigureAwait(false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }
    }
}
