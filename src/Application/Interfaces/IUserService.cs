using Application.Common.Results;
using Application.DTOs.Requests;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Result> AddUserAsync(AddUserRequest request);
        Task<Result<List<User>>> GetAllUsersAsync();
    }
}
