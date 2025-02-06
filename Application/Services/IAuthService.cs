using Application.Common.Results;
using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Services
{
    public interface IAuthService
    {
        Task<Result> RegisterUserAsync(RegisterUserRequest request);
        Task<Result<OtpResponseDto>> SendMobileOtpAsync(SendOtpRequest request);
        Task<Result> VerifyMobileOtpAsync(VerifyOtpRequest request);
        Task<Result<OtpResponseDto>> SendEmailOtpAsync(SendOtpRequest request);
        Task<Result> VerifyEmailOtpAsync(VerifyOtpRequest request);
        Task<Result> AcceptPrivacyPolicyAsync(AcceptPrivacyPolicyRequest request);
        Task<Result> RegisterDeviceAsync(RegisterDevicePinRequest request);
        // Task<Result<LoginResponseDto>> LoginWithPinAsync(LoginWithPinRequest request);
    }
}
