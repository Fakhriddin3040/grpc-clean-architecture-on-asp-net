namespace AuthMicroservice.Infrastructure.DependencyInjection;

using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<AuthDbContext>(options =>
		{
			options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
		});

		services.AddScoped<IAuthDbContext>(provider => provider.GetService<AuthDbContext>()!);
		services.AddScoped<IConfiguration>(provider => configuration);

		return services;
	}
}