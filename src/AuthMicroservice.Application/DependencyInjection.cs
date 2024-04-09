using AuthMicroservice.Application.Common.Mapping;
using AuthMicroservice.Application.Services;
using AuthMicroservice.Domain.Configurations;
using AuthMicroservice.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthMicroservice.Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddGrpc();
            services.AddGrpcReflection();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordService, PasswordService>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtOptions.ISSUER,
                        ValidAudience = JwtOptions.AUDIENCE,
                        IssuerSigningKey = JwtOptions.GetSymmetricSecurityKey()
                    };
                });

            return services;
        }

        public static void UseApplication(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<JwTokenAuthenticationGrpcService>();

                endpoints.MapGrpcReflectionService();
            });
        }
    }
}