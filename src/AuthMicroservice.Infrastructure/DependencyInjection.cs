
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Infrastructure.DataAccess;
using AuthMicroservice.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthMicroservice.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuthDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAuthDbContext>(provider => provider.GetService<AuthDbContext>()!);

        return services;
    }
}