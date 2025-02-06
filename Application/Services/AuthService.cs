using System.Text.Json;
using Application.Common.Helpers;
using Application.Common.Results;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Models;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCacheService _memoryCacheService;
        // private readonly IDeviceRepository _deviceRepository;
        // private readonly IOtpService _otpService;

        public AuthService(IUserRepository userRepository, IMemoryCacheService memoryCacheService)
        {
            _userRepository = userRepository;
            _memoryCacheService = memoryCacheService;
            // _deviceRepository = deviceRepository;
            // _otpService = otpService;
        }

        public async Task<Result> RegisterUserAsync(RegisterUserRequest request)
        {
            var userExists = await _userRepository.ExistsByICNumberAsync(request.ICNumber);
            if (userExists)
            {
                return Result.Failure(Error.UserNameAlreadyExists);
            }

            var user = new User(request.Name, request.ICNumber, request.Mobile, request.Email);
            await _userRepository.AddAsync(user);

            return Result.Success();
        }

        // public async Task<Result> RegisterDeviceAsync(RegisterDeviceRequest request)
        // {
        //     var deviceRegistration = new DeviceRegistration ();

        // }

        public async Task<Result<OtpResponseDto>> SendMobileOtpAsync(SendOtpRequest request)
        {
            var user = await _userRepository.GetUserByICNumber(request.ICNumber);
            if (user == null)
            {
                return Result<OtpResponseDto>.Failure<OtpResponseDto>(Error.UserNotFound);
            }

            var deviceKey = request.DeviceId;
            var otpKey = OtpHelper.BuildKey(request.DeviceId, OtpHelper.OtpChannel.Mobile);

            var otpPin = OtpHelper.GenerateOtpPin();
            var maskedContact = MaskingHelper.MaskPhone(user.Mobile);

            var otp = _memoryCacheService.Get<Otp>(otpKey)
                        ?? new Otp(otpPin);
            var device = _memoryCacheService.Get<DeviceRegistration>(deviceKey)
                            ?? new DeviceRegistration(user.Id, request.DeviceId);

            // send otp to user
            
            _memoryCacheService.Set(deviceKey, device);
            _memoryCacheService.Set(otpKey, otp, TimeSpan.FromMinutes(5));

            var otpResponse = new OtpResponseDto { MaskedContact = maskedContact };
            return Result<OtpResponseDto>.Success<OtpResponseDto>(otpResponse);
        }

        // public async Task<ResponseDto> VerifyMobileOtpAsync(VerifyOtpRequest request)
        // {
        //     return await _otpService.VerifyOtpAsync(request.OtpToken, request.Otp);
        // }

        public async Task<Result<OtpResponseDto>> SendEmailOtpAsync(SendOtpRequest request)
        {
            var user = await _userRepository.GetUserByICNumber(request.ICNumber);
            if (user == null)
            {
                return Result<OtpResponseDto>.Failure<OtpResponseDto>(Error.UserNotFound);
            }

            var deviceKey = request.DeviceId;
            var otpKey = OtpHelper.BuildKey(request.DeviceId, OtpHelper.OtpChannel.Email);

            var otpPin = OtpHelper.GenerateOtpPin();
            var maskedContact = MaskingHelper.MaskPhone(user.Mobile);

            var otp = _memoryCacheService.Get<Otp>(otpKey)
                        ?? new Otp(otpPin);
            var device = _memoryCacheService.Get<DeviceRegistration>(deviceKey)
                            ?? new DeviceRegistration(user.Id, request.DeviceId);

            // send otp to user

            _memoryCacheService.Set(deviceKey, device);
            _memoryCacheService.Set(otpKey, otp, TimeSpan.FromMinutes(5));

            var otpResponse = new OtpResponseDto { MaskedContact = maskedContact };
            return Result<OtpResponseDto>.Success<OtpResponseDto>(otpResponse);
        }

        // public async Task<ResponseDto> VerifyEmailOtpAsync(VerifyOtpRequest request)
        // {
        //     return await _otpService.VerifyOtpAsync(request.OtpToken, request.Otp);
        // }

        // public async Task<ResponseDto> SetDevicePinAsync(SetDevicePinRequest request)
        // {
        //     await _deviceRepository.SetDevicePinAsync(request.ICNumber, request.DeviceId, request.Pin);
        //     return new ResponseDto { Success = true, Message = "PIN updated successfully" };
        // }

        // public async Task<LoginResponseDto> LoginWithPinAsync(LoginWithPinRequest request)
        // {
        //     return await _deviceRepository.ValidateDeviceLoginAsync(request.ICNumber, request.DeviceId, request.Pin);
        // }
    }
}
