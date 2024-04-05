using Microsoft.EntityFrameworkCore;
using AuthMicroservice.Domain.Entities;

namespace AuthMicroservice.Domain.Interfaces
{
	public interface IAuthDbContext
	{
		public DbSet<TEntity> Set<TEntity>() where TEntity : class;
		public int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}