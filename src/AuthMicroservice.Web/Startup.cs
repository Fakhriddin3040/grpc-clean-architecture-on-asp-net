using System.Text;
using AuthMicroservice.Application;
using AuthMicroservice.Application.Common.Mapping;
using AuthMicroservice.Application.Services;
using AuthMicroservice.Domain.Configurations;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Domain.Interfaces.Services;
using AuthMicroservice.Infrastructure.DataAccess;
using AuthMicroservice.Infrastructure.DataAccess.Repositories;
using AuthMicroservice.Infrastructure.DependencyInjection;
using AutoMapper;
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
			services.AddGrpc();
			services.AddGrpcReflection();
			services.AddAutoMapper(typeof(MappingProfile));
			services.AddScoped<IUserService, UserService>();
			services.AddInfrastructure(_configuration);

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
				endpoints.MapGrpcService<JwTokenAuthenticationGrpcService>();
				endpoints.MapGrpcReflectionService();
			});
		}
	}
}
