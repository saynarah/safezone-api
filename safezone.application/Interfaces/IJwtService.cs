using safezone.domain.Entities;

namespace safezone.application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
