using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> ExistsByICNumberAsync(string icNumber)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.ICNumber == icNumber);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async  Task<User?> GetUserByICNumber(string icNumber)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.ICNumber == icNumber);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
    }
}
