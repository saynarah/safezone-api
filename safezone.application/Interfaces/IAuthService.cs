namespace safezone.application.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string senha);
        bool VerifyPassword(string senhaDigitada, string senhaHash);
    }
}
