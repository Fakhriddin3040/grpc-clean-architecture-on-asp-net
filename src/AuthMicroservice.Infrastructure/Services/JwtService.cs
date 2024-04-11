using System.IdentityModel.Tokens.Jwt;
using AuthMicroservice.Domain.Configurations;
using AuthMicroservice.Domain.Interfaces.Services;

namespace AuthMicroservice.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateAccessToken(Guid userId)
        {
            return WriteToken(GetJwtSecurityToken(userId));
        }

        public JwtSecurityToken GetJwtSecurityToken(Guid userId)
        {
            return new JwtSecurityToken(
                issuer: JwtOptions.ISSUER,
                audience: JwtOptions.AUDIENCE,
                claims: JwtOptions.GetClaims(userId),
                expires: JwtOptions.GetExpireTime(),
                signingCredentials: JwtOptions.GetSigningCredentials()
            );
        }

        public string WriteToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GetTokenFromRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public string GetUserIdFromRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public string GetUserIdFromToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool IsRefreshTokenExpired(string token)
        {
            throw new NotImplementedException();
        }

        public bool IsTokenExpired(string token)
        {
            throw new NotImplementedException();
        }

        public bool IsValidRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool IsValidToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}