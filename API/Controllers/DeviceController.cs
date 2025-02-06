using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController(IDeviceRepository deviceRepository) : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository = deviceRepository;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _deviceRepository.GetAllDevicesAsync();
            return Ok(users);
        }
    }
}
