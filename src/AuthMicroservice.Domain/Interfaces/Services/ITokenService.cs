namespace AuthMicroservice.Domain.Interfaces.Services
{
	public interface ITokenService
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

		string GetTokenFromToken(string token);

		string GetTokenFromToken(string token, string secret);
	}
}