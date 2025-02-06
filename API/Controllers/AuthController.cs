using API.Extensions;
using Application.DTOs.Requests;
using Application.Services;
using Microsoft.AspNetCore.Mvc;



namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register-user")]
        public async Task<IResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var response = await _authService.RegisterUserAsync(request);
            return response.ToHttpResponse();
        }

        // [HttpPost("initiate-device-registration")]
        // public async Task<IResult> RegisterDevice([FromBody] RegisterDeviceRequest request)
        // {
        //     var response = await _authService.RegisterDeviceAsync(request);
        //     return response.ToHttpResponse();
        // }

        [HttpPost("send-mobile-otp")]
        public async Task<IResult> SendMobileOtp([FromBody] SendOtpRequest request)
        {
            var response = await _authService.SendMobileOtpAsync(request);
            return response.ToHttpResponse();
        }

        // [HttpPost("verify-mobile-otp")]
        // public async Task<IResult> VerifyMobileOtp([FromBody] VerifyOtpRequest request)
        // {
        //     var response = await _authService.VerifyMobileOtpAsync(request);
        //     return response.ToHttpResponse();
        // }

        [HttpPost("send-email-otp")]
        public async Task<IResult> SendEmailOtp([FromBody] SendOtpRequest request)
        {
            var response = await _authService.SendEmailOtpAsync(request);
            return response.ToHttpResponse();
        }

        // [HttpPost("verify-email-otp")]
        // public async Task<IResult> VerifyEmailOtp([FromBody] VerifyOtpRequest request)
        // {
        //     var response = await _authService.VerifyEmailOtpAsync(request);
        //     return response.ToHttpResponse();
        // }

        // [HttpPost("set-device-pin")]
        // public async Task<IResult> SetDevicePin([FromBody] SetDevicePinRequest request)
        // {
        //     var response = await _authService.SetDevicePinAsync(request);
        //     return response.ToHttpResponse();
        // }

        // [HttpPost("login-with-pin")]
        // public async Task<IResult> LoginWithPin([FromBody] LoginWithPinRequest request)
        // {
        //     var response = await _authService.LoginWithPinAsync(request);
        //     return response.ToHttpResponse();
        // }
    }
}
