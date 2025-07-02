using Microsoft.AspNetCore.Identity;
using safezone.application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
