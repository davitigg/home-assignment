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
    public class AuthService(
        IUserRepository userRepository,
        IDeviceRepository deviceRepository,
        IMemoryCacheService memoryCacheService,
        IHashingService hashingService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IDeviceRepository _deviceRepository = deviceRepository;
        private readonly IMemoryCacheService _memoryCacheService = memoryCacheService;
        private readonly IHashingService _hashingService = hashingService;

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

        public async Task<Result<OtpResponseDto>> SendMobileOtpAsync(SendOtpRequest request)
        {
            var user = await _userRepository.GetUserByICNumber(request.ICNumber);
            if (user == null)
            {
                return Result<OtpResponseDto>.Failure<OtpResponseDto>(Error.UserNotFound);
            }

            var otpKey = OtpHelper.BuildMobileOtpKey(user.Id.ToString());

            var cachedOtp = _memoryCacheService.Get<Otp>(otpKey);
            if (cachedOtp != null && !cachedOtp.ResendPermitted)
            {
                return Result<OtpResponseDto>.Failure<OtpResponseDto>(Error.OtpResendNotPermitted);
            }

            var otpPin = OtpHelper.GenerateOtpPin();
            var otp = new Otp(user.Id, otpPin);

            var deviceKey = user.Id.ToString();
            var device = _memoryCacheService.Get<DeviceRegistration>(deviceKey)
                            ?? new DeviceRegistration(request.DeviceId);

            // TODO: Implement OPT sending
            Console.WriteLine($"Mobile OTP: {otpPin}");

            _memoryCacheService.Set(deviceKey, device);
            _memoryCacheService.Set(otpKey, otp, TimeSpan.FromMinutes(5));

            var maskedContact = MaskingHelper.MaskPhone(user.Mobile);
            var otpResponse = new OtpResponseDto { MaskedContact = maskedContact };

            return Result<OtpResponseDto>.Success<OtpResponseDto>(otpResponse);
        }

        public async Task<Result> VerifyMobileOtpAsync(VerifyOtpRequest request)
        {
            var user = await _userRepository.GetUserByICNumber(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var deviceKey = user.Id.ToString();
            var cachedDevice = _memoryCacheService.Get<DeviceRegistration>(deviceKey);
            if (cachedDevice == null)
            {
                return Result.Failure(Error.DeviceNotFoundOrSessionExpired);
            }

            var otpKey = OtpHelper.BuildMobileOtpKey(user.Id.ToString());

            var cachedOtp = _memoryCacheService.Get<Otp>(otpKey);
            if (cachedOtp != null && cachedOtp.Pin == request.OtpPin)
            {
                cachedDevice.MobileVerified = true;
                _memoryCacheService.Set(deviceKey, cachedDevice);

                _memoryCacheService.Remove(otpKey);
                return Result.Success();
            }

            return Result.Failure(Error.OtpInvalidOrExpired);
        }

        public async Task<Result<OtpResponseDto>> SendEmailOtpAsync(SendOtpRequest request)
        {
            var user = await _userRepository.GetUserByICNumber(request.ICNumber);
            if (user == null)
            {
                return Result<OtpResponseDto>.Failure<OtpResponseDto>(Error.UserNotFound);
            }

            var otpKey = OtpHelper.BuildEmailOtpKey(user.Id.ToString());

            var cachedOtp = _memoryCacheService.Get<Otp>(otpKey);
            if (cachedOtp != null && !cachedOtp.ResendPermitted)
            {
                return Result<OtpResponseDto>.Failure<OtpResponseDto>(Error.OtpResendNotPermitted);
            }

            var otpPin = OtpHelper.GenerateOtpPin();
            var otp = new Otp(user.Id, otpPin);

            var deviceKey = user.Id.ToString();
            var device = _memoryCacheService.Get<DeviceRegistration>(deviceKey)
                            ?? new DeviceRegistration(request.DeviceId);

            // TODO: Implement OPT sending
            Console.WriteLine($"Email OTP: {otpPin}");

            _memoryCacheService.Set(deviceKey, device);
            _memoryCacheService.Set(otpKey, otp, TimeSpan.FromMinutes(5));

            var maskedContact = MaskingHelper.MaskPhone(user.Email);
            var otpResponse = new OtpResponseDto { MaskedContact = maskedContact };

            return Result<OtpResponseDto>.Success<OtpResponseDto>(otpResponse);
        }

        public async Task<Result> VerifyEmailOtpAsync(VerifyOtpRequest request)
        {
            var user = await _userRepository.GetUserByICNumber(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var deviceKey = user.Id.ToString();
            var cachedDevice = _memoryCacheService.Get<DeviceRegistration>(deviceKey);
            if (cachedDevice == null)
            {
                return Result.Failure(Error.DeviceNotFoundOrSessionExpired);
            }

            var otpKey = OtpHelper.BuildEmailOtpKey(user.Id.ToString());

            var cachedOtp = _memoryCacheService.Get<Otp>(otpKey);
            if (cachedOtp != null && cachedOtp.Pin == request.OtpPin)
            {
                cachedDevice.EmailVerified = true;
                _memoryCacheService.Set(deviceKey, cachedDevice);

                _memoryCacheService.Remove(otpKey);
                return Result.Success();
            }

            return Result.Failure(Error.OtpInvalidOrExpired);
        }

        public async Task<Result> AcceptPrivacyPolicyAsync(AcceptPrivacyPolicyRequest request)
        {
            var user = await _userRepository.GetUserByICNumber(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var deviceKey = user.Id.ToString();
            var cachedDevice = _memoryCacheService.Get<DeviceRegistration>(deviceKey);
            if (cachedDevice == null)
            {
                return Result.Failure(Error.DeviceNotFoundOrSessionExpired);
            }

            cachedDevice.PrivacyPolicyAccepted = true;
            _memoryCacheService.Set(deviceKey, cachedDevice);

            return Result.Success();
        }

        public async Task<Result> RegisterDeviceAsync(RegisterDevicePinRequest request)
        {
            var user = await _userRepository.GetUserByICNumber(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var deviceKey = user.Id.ToString();
            var cachedDevice = _memoryCacheService.Get<DeviceRegistration>(deviceKey);
            if (cachedDevice == null || cachedDevice.DeviceId != request.DeviceId)
            {
                return Result.Failure(Error.DeviceNotFoundOrSessionExpired);
            }

            var hash = _hashingService.GenerateHash(request.DeviceId, request.Pin);
            var device = new Device(user.Id, cachedDevice.DeviceId, hash);
            await _deviceRepository.AddOrUpdateDeviceAsync(device);

            return Result.Success();
        }

        // public async Task<LoginResponseDto> LoginWithPinAsync(LoginWithPinRequest request)
        // {
        //     return await _deviceRepository.ValidateDeviceLoginAsync(request.ICNumber, request.DeviceId, request.Pin);
        // }
    }
}
