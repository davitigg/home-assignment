using API.Extensions;
using Application.DTOs.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IResult> AddUser([FromBody] AddUserRequest request)
        {
            var response = await _userService.AddUserAsync(request);
            return response.ToHttpResponse();
        }

        // for testing purposes
        [HttpGet]
        public async Task<IResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            return response.ToHttpResponse();
        }
    }
}
