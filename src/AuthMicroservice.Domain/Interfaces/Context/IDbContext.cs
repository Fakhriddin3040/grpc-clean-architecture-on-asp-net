using Microsoft.EntityFrameworkCore;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces
{
	public interface IAuthDbContext
	{
		public DbSet<User> Users { get; }
		public int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}