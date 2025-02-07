using Application.Common.Results;
using Application.Common.Settings;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class OtpService(
        IOtpRepository otpRepository,
        IUserRepository userRepository,
        IOptions<ExpirationSettings> expirationSettings) : IOtpService
    {
        private readonly IOtpRepository _otpRepository = otpRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly TimeSpan _expirationDuration = TimeSpan.FromMinutes(expirationSettings.Value.OtpLifetimeMinutes);

        public async Task<Result<OtpResponseDto>> SendOtpMobileAsync(SendOtpRequest request)
        {
            return await GenerateAndSendOtpAsync(request.ICNumber, OtpType.Mobile);
        }

        public async Task<Result<OtpResponseDto>> SendOtpEmailAsync(SendOtpRequest request)
        {
            return await GenerateAndSendOtpAsync(request.ICNumber, OtpType.Email);
        }

        public Result<List<OtpEntry>> GetAllOtpEntries()
        {
            var otpEntries = _otpRepository.GetAll();
            return Result<List<OtpEntry>>.Success(otpEntries);
        }

        private async Task<Result<OtpResponseDto>> GenerateAndSendOtpAsync(string icNumber, OtpType otpType)
        {
            var user = await _userRepository.GetByICNumberAsync(icNumber);
            if (user == null)
            {
                return Result<OtpResponseDto>.Failure<OtpResponseDto>(Error.UserNotFound);
            }

            string contact = otpType == OtpType.Mobile ? user.Mobile : user.Email;
            var otpEntry = new OtpEntry(user.Id, contact, otpType, _expirationDuration);

            Console.WriteLine($"{otpType} OTP: {otpEntry.Code}"); // TODO: Implement actual OTP sending

            _otpRepository.Set(otpEntry.Id, otpEntry);

            var otpResponse = new OtpResponseDto(contact, otpType);
            return Result<OtpResponseDto>.Success<OtpResponseDto>(otpResponse);
        }
    }
}
