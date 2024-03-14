using AutoMapper;
using EmployeeManagement.Business.Abstract;
using EmployeeManagement.DataAccess.Domain;
using EmployeeManagement.WebAPI.Dtos.User;
using EmployeeManagement.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var list = await _userService.GetAllAsync().ConfigureAwait(false);

                if (list.HasValue())
                {
                    return Ok(new { IsSuccess = true, data = list.ToList() });
                }
                return NotFound(new { IsSuccess = false, message = "Kullanıcı bulunamadı" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("get-user")]
        public async Task<IActionResult> GetUserById([FromBody]GetUserByIdDto getUserByIdDto)
        {
            try
            {
                var data = await _userService.GetByIdAsync(getUserByIdDto.Id).ConfigureAwait(false);
                if (data != null)
                {
                    return Ok(new { IsSuccess = true, data = data });
                }
                return NotFound(new { IsSuccess = true, message = "Kullanıcı bulunamadı."});
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, ex.Message });
            }
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserDto createUserDto)
        {
            try
            {
                var mappedData = _mapper.Map<User>(createUserDto);
                await _userService.AddAsync(mappedData).ConfigureAwait(false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDto updateUserDto)
        {
            try
            {
                var foundData = await _userService.GetByIdAsync(updateUserDto.Id).ConfigureAwait(false);
                if (foundData == null)
                {
                    return NotFound(new { IsSuccess = false, message = "Kullanıcı bulunamadı." });
                }
                foundData.Name = updateUserDto.Name;
                foundData.Surname = updateUserDto.Surname;
                foundData.Username = updateUserDto.Username;
                foundData.Email = updateUserDto.Email;
                await _userService.UpdateAsync(foundData).ConfigureAwait(false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new {IsSuccess = false, message = ex.Message});
            }
        }

        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser([FromBody]DeleteUserDto deleteUserDto)
        {
            try
            {
                var foundData = await _userService.GetByIdAsync(deleteUserDto.Id).ConfigureAwait(false);
                if(foundData == null)
                {
                    return NotFound(new { IsSuccess = false, message = "Kullanıcı bulunamadı." });
                }
                await _userService.DeleteAsync(foundData).ConfigureAwait(false);
                return Ok(new { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, message = ex.Message });
            }
        }
    }
}
