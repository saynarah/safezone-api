using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safezone.application.Configurations
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = "Safezone";
        public string Audience { get; set; } = "SafezoneUsers";
        public int ExpirationInMinutes { get; set; } = 60;
    }
}
