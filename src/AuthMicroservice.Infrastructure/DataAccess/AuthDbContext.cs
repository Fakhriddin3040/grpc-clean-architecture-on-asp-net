using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Infrastructure.DataAccess;
public class AuthDbContext : DbContext, IAuthDbContext
{
	public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
	{}

	public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new AuthEntitiesConfiguration());
		base.OnModelCreating(builder);
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(b =>
		{
			b.MigrationsAssembly("AuthMicroservice.Web");
		});
		base.OnConfiguring(optionsBuilder);
	}

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await base.SaveChangesAsync(cancellationToken);
	}
}