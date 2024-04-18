namespace AuthMicroservice.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(Guid userId);

        string GenerateRefreshToken();

        string GetUserIdFromToken(string token);

        string GetUserIdFromRefreshToken(string token);

        bool IsValidToken(string token);

        bool IsValidRefreshToken(string token);

        bool IsTokenExpired(string token);

        bool IsRefreshTokenExpired(string token);

        string GetTokenFromRefreshToken(string token);
    }
}