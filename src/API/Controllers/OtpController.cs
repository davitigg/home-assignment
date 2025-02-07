using API.Extensions;
using Application.DTOs.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController(IOtpService otpService) : ControllerBase
    {
        private readonly IOtpService _otpService = otpService;

        [HttpPost("mobile/send")]
        public async Task<IResult> SendOtpMobile([FromBody] SendOtpRequest request)
        {
            var response = await _otpService.SendOtpMobileAsync(request);
            return response.ToHttpResponse();
        }

        [HttpPost("email/send")]
        public async Task<IResult> SendOtpEmail([FromBody] SendOtpRequest request)
        {
            var response = await _otpService.SendOtpEmailAsync(request);
            return response.ToHttpResponse();
        }
        
        // for testing purposes
        [HttpGet()]
        public IResult GetAllOtpEntries()
        {
            var response = _otpService.GetAllOtpEntries();
            return response.ToHttpResponse();
        }
    }
}
