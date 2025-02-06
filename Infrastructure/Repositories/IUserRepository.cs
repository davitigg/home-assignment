using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsByICNumberAsync(string icNumber);
        Task AddAsync(User user);
        Task<User?> GetUserByICNumber(string icNumber);
        Task<List<User>> GetAllUsersAsync();
    }
}
