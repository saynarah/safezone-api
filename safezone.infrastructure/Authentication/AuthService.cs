using Microsoft.AspNetCore.Identity;
using safezone.application.Interfaces;

namespace safezone.infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly PasswordHasher<string> _hasher = new();

        public string HashPassword(string senha) 
            => _hasher.HashPassword(null, senha);

        public bool VerifyPassword(string senhaDigitada, string senhaHash) 
            => _hasher.VerifyHashedPassword(null, senhaHash, senhaDigitada)
                == PasswordVerificationResult.Success;
    }
}
