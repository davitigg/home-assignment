using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
