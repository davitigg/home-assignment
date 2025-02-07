using Application.Common.Results;
using Application.Common.Settings;
using Application.DTOs.Requests;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class DeviceService(
        IDeviceRepository deviceRepository,
        IUserRepository userRepository,
        IPendingDeviceRepository pendingDeviceRepository,
        IOtpRepository otpRepository,
        IHashingService hashingService,
        IOptions<ExpirationSettings> expirationSettings) : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository = deviceRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPendingDeviceRepository _pendingDeviceRepository = pendingDeviceRepository;
        private readonly IOtpRepository _otpRepository = otpRepository;
        private readonly IHashingService _hashingService = hashingService;
        private readonly TimeSpan _expirationDuration = TimeSpan.FromMinutes(expirationSettings.Value.PendingDeviceLifetimeMinutes);


        public async Task<Result> AddPendingDeviceAsync(AddPendingDeviceRequest request)
        {
            var user = await _userRepository.GetByICNumberAsync(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var pendingDevice = new PendingDevice(user.Id, request.DeviceId, _expirationDuration);

            _pendingDeviceRepository.Set(pendingDevice.Id, pendingDevice);

            return Result.Success();
        }

        public async Task<Result> VerifyMobileOtpAsync(VerifyOtpRequest request)
        {
            return await VerifyOtpAsync(request, OtpType.Mobile);
        }

        public async Task<Result> VerifyEmailOtpAsync(VerifyOtpRequest request)
        {
            return await VerifyOtpAsync(request, OtpType.Email);
        }

        public async Task<Result> AcceptPrivacyPolicyAsync(AcceptPrivacyPolicyRequest request)
        {
            var user = await _userRepository.GetByICNumberAsync(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var pendingDevice = _pendingDeviceRepository.Get(user.Id.ToString());
            if (pendingDevice == null)
            {
                return Result.Failure(Error.PendingDeviceNotFound);
            }

            pendingDevice.AcceptPrivacyPolicy();
            _pendingDeviceRepository.Set(pendingDevice.Id, pendingDevice);

            return Result.Success();
        }

        public async Task<Result> AddDeviceAsync(AddDeviceRequest request)
        {
            var user = await _userRepository.GetByICNumberAsync(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var pendingDevice = _pendingDeviceRepository.Get(user.Id.ToString());
            if (pendingDevice == null || pendingDevice.DeviceId != request.DeviceId)
            {
                return Result.Failure(Error.PendingDeviceNotFound);
            }

            if (!pendingDevice.CanBeRegistered())
            {
                return Result.Failure(Error.DeviceVerificationIncomplete);
            }

            var hash = _hashingService.GeneratePinHash(request.DeviceId, request.Pin);
            var device = new Device(user.Id, pendingDevice.DeviceId, hash);
            await _deviceRepository.AddAsync(device);
            _pendingDeviceRepository.Remove(pendingDevice.Id);

            return Result.Success();
        }

        public async Task<Result> EnableBiometrics(EnableBiometricsRequest request)
        {
            var user = await _userRepository.GetByICNumberAsync(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var device = await _deviceRepository.GetByUserIdAndDeviceIdAsync(user.Id, request.DeviceId);
            if (device == null)
            {
                return Result.Failure(Error.DeviceNotFound);
            }

            var hash = _hashingService.GenerateBiometricHash(request.DeviceId, request.BiometricData);
            device.EnableBiometrics(hash);
            await _deviceRepository.UpdateAsync(device);

            return Result.Success();
        }

        public async Task<Result<List<Device>>> GetAllDevicesAsync()
        {
            var devices = await _deviceRepository.GetAllAsync();
            return Result<List<Device>>.Success(devices);
        }

        public Result<List<PendingDevice>> GetAllPendingDevices()
        {
            var pendingDevices = _pendingDeviceRepository.GetAll();
            return Result<List<PendingDevice>>.Success(pendingDevices);
        }

        public async Task<Result> VerifyOtpAsync(VerifyOtpRequest request, OtpType otpType)
        {
            var user = await _userRepository.GetByICNumberAsync(request.ICNumber);
            if (user == null)
            {
                return Result.Failure(Error.UserNotFound);
            }

            var pendingDevice = _pendingDeviceRepository.Get(user.Id.ToString());
            if (pendingDevice == null)
            {
                return Result.Failure(Error.PendingDeviceNotFound);
            }

            var otpEntry = _otpRepository.Get($"{user.Id}:{otpType}");
            if (otpEntry == null)
            {
                return Result.Failure(Error.OtpNotFound);
            }

            if (otpEntry.Code != request.Code)
            {
                return Result.Failure(Error.InvalidOtp);
            }

            switch (otpType)
            {
                case OtpType.Mobile: pendingDevice.VerifyMobile(); break;
                case OtpType.Email: pendingDevice.VerifyEmail(); break;
                default: throw new NotImplementedException();
            }

            _pendingDeviceRepository.Set(pendingDevice.Id, pendingDevice);
            _otpRepository.Remove(otpEntry.Id);

            return Result.Success();
        }
    }
}