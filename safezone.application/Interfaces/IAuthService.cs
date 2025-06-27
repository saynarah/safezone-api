using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safezone.application.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string senha);
        bool VerifyPassword(string senhaDigitada, string senhaHash);
    }
}
