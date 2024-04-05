using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.Interfaces.Repositories;
using AuthMicroservice.Infrastructure.DataAccess.Repositories;
using AuthMicroservice.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Infrastructure.DataAccess;
public class AuthDbContext : DbContext, IAuthDbContext
{
	public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
	{
	}

	public IRepository<IUser> Users => new UserRepository(this);

    protected override void OnModelCreating(ModelBuilder builder)
    {
		builder.ApplyConfiguration(new AuthEntitiesConfiguration());
		base.OnModelCreating(builder);
    }
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await base.SaveChangesAsync(cancellationToken);
	}
}