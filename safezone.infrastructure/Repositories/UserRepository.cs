using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using safezone.application.Interfaces;
using safezone.domain.Entities;
using safezone.infrastructure.Persistence;

namespace safezone.infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) 
            => _context = context;

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) 
                throw new KeyNotFoundException($"User with ID {id} not found.");
                
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> GetUserByEmailAsync(string email) 
            => await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
    }
}
