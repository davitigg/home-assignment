using API.Extensions;
using Application.DTOs.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController(IDeviceService deviceService) : ControllerBase
    {
        private readonly IDeviceService _deviceService = deviceService;

        [HttpPost("pending")]
        public async Task<IResult> AddPendingDevice(AddPendingDeviceRequest request)
        {
            var response = await _deviceService.AddPendingDeviceAsync(request);
            return response.ToHttpResponse();
        }

        [HttpPost("pending/verify-mobile")]
        public async Task<IResult> VerifyMobileOtp(VerifyOtpRequest request)
        {
            var response = await _deviceService.VerifyMobileOtpAsync(request);
            return response.ToHttpResponse();
        }

        [HttpPost("pending/verify-email")]
        public async Task<IResult> VerifyEmailOtp(VerifyOtpRequest request)
        {
            var response = await _deviceService.VerifyEmailOtpAsync(request);
            return response.ToHttpResponse();
        }

        [HttpPost("pending/accept-privacy-policy")]
        public async Task<IResult> AcceptPrivacyPolicy(AcceptPrivacyPolicyRequest request)
        {
            var response = await _deviceService.AcceptPrivacyPolicyAsync(request);
            return response.ToHttpResponse();
        }

        [HttpPost]
        public async Task<IResult> AddDevice(AddDeviceRequest request)
        {
            var response = await _deviceService.AddDeviceAsync(request);
            return response.ToHttpResponse();
        }

        [HttpPost("enable-biometrics")]
        public async Task<IResult> EnableBiometrics(EnableBiometricsRequest request)
        {
            var response = await _deviceService.EnableBiometrics(request);
            return response.ToHttpResponse();
        }

        // for testing purposes
        [HttpGet]
        public async Task<IResult> GetAllDevices()
        {
            var response = await _deviceService.GetAllDevicesAsync();
            return response.ToHttpResponse();
        }

        // for testing purposes
        [HttpGet("pending")]
        public IResult GetAllPendingDevices()
        {
            var response = _deviceService.GetAllPendingDevices();
            return response.ToHttpResponse();
        }
    }
}
