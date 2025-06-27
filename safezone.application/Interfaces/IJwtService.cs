using safezone.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safezone.application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
