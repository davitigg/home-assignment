using Application.Common.Results;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOtpService
    {
        Task<Result<OtpResponseDto>> SendOtpMobileAsync(SendOtpRequest sendOtpRequest);
        Task<Result<OtpResponseDto>> SendOtpEmailAsync(SendOtpRequest request);
        Result<List<OtpEntry>> GetAllOtpEntries();
    }
}