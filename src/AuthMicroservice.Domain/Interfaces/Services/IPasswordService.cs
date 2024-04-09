namespace AuthMicroservice.Domain.Interfaces.Services
{
    public interface IPasswordService
    {
        string GenerateSalt();

        string HashPassword(string password, string salt);

        bool VerifyPassword(string text, string hashed, string salt);

        string GeneratePassword(int length);
    }
}