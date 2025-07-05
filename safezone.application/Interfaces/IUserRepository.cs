using safezone.domain.Entities;

namespace safezone.application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ValidateUserAsync(string email, string password);
        Task<User> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
    }
}
