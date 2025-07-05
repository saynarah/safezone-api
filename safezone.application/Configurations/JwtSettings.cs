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
