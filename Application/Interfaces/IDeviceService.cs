using Application.Common.Results;
using Application.DTOs.Requests;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDeviceService
    {
        Task<Result> AddPendingDeviceAsync(AddPendingDeviceRequest request);
        Task<Result> VerifyMobileOtpAsync(VerifyOtpRequest request);
        Task<Result> VerifyEmailOtpAsync(VerifyOtpRequest request);
        Task<Result> AcceptPrivacyPolicyAsync(AcceptPrivacyPolicyRequest request);
        Task<Result> AddDeviceAsync(AddDeviceRequest request);
        Task<Result> EnableBiometrics(EnableBiometricsRequest request);
        Task<Result<List<Device>>> GetAllDevicesAsync();
        Result<List<PendingDevice>> GetAllPendingDevices();
    }
}