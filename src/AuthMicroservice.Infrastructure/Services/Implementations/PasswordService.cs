using AuthMicroservice.Domain.Configurations;
using AuthMicroservice.Infrastructure.Interfaces.Services;

namespace AuthMicroservice.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public string GeneratePassword(int length = 5)
        {
            string randChars = "";

            for (int i = 0; i < length; i++)
            {
                randChars += (char)new Random().Next(33, 126);
            }

            return randChars;
        }

        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(JwtOptions.SaltWorkFactor);
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyPassword(string password, string salt, string hashed)
        {
            return HashPassword(password: password, salt: salt) == hashed;
        }
    }
}
