using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;

namespace AuthMicroservice.Domain.Configurations
{
    public static class JwtAuthOptions
    {
        public const string KEY = "Very_Secret_Kdsfjfjsfhsjdfhsjdfhskfhsjdkkfhskjdfey";

        public const int LIFETIME = 5000;
        
        public const string Issuer = "AuthMicroservice";

        public const string Audience = "AuthMicroservice";

        public static DateTime GetExpireTime() =>
			DateTime.Now.AddDays(120);

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}