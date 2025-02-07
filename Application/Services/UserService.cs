using Application.Common.Results;
using Application.DTOs.Requests;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result> AddUserAsync(AddUserRequest request)
        {
            var user = await _userRepository.GetByICNumberAsync(request.ICNumber);
            if (user != null)
            {
                return Result.Failure(Error.UserNameAlreadyExists);
            }

            user = new User(request.Name, request.ICNumber, request.Mobile, request.Email);
            await _userRepository.AddAsync(user);

            return Result.Success();
        }

        public async Task<Result<List<User>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return Result<List<User>>.Success(users);
        }
    }
}