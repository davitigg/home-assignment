using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByICNumberAsync(string icNumber)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.ICNumber == icNumber);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
    }
}
