using System.Text;
using AuthMicroservice.Application;
using AuthMicroservice.Application.Services;
using AuthMicroservice.Domain.Configurations;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Domain.Interfaces.Services;
using AuthMicroservice.Infrastructure.DataAccess;
using AuthMicroservice.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthMicroservice.Web
{
	public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AuthDbContext>(options =>
				{
					options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"), b =>
					{
						b.MigrationsAssembly("AuthMicroservice.Web");
					});
				});
			services.AddGrpc();
			services.AddGrpcReflection();


			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<AuthDbContext>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = JwtAuthOptions.ISSUER,
						ValidAudience = JwtAuthOptions.AUDIENCE,
						IssuerSigningKey = JwtAuthOptions.GetSymmetricSecurityKey()
					};
				});

			services.AddAuthorization();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGrpcService<JwtAuthService>();
				endpoints.MapGrpcReflectionService();
			});
		}
	}
}
