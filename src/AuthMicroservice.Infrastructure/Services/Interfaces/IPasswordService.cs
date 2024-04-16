namespace AuthMicroservice.Infrastructure.Interfaces.Services
{
    public interface IPasswordService
    {
        string GenerateSalt();

        string HashPassword(string password, string salt);

        bool VerifyPassword(string password, string hashed, string salt);

        string GeneratePassword(int length);
    }
}