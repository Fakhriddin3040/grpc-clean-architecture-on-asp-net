using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;

namespace AuthMicroservice.Domain.Configurations
{
    public static class JwtAuthOptions
    {
        public const string KEY = "kjladfsjksadiofwaioefhiuaiodjsfhiouadsfhoisklfxcvxdfghiosuiuasfhiuwcjnviosfjiopsdifoiosudfjdiosjfiosajfiouahsvoufrbhnviudfbhnviudfbhviudohjnvuedhrfiouv";

        public const int LIFETIME = 5000;
        
        public const string ISSUER = "AuthMicroservice";

        public const string AUDIENCE = "AuthMicroservice";


        public const string SecurityAlgorithm = SecurityAlgorithms.HmacSha256;

        public static DateTime GetExpireTime() =>
            DateTime.Now.AddDays(120);

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

        public static List<Claim> GetClaims(Guid userId) => new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userId.ToString()),
        };
    }
}