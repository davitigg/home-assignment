using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByICNumberAsync(string icNumber);
        Task<List<User>> GetAllAsync();
    }
}
