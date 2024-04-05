using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Infrastructure;

public class DbInitializer
{
	public static void Initialize(IAuthDbContext context)
	{
		context.Set<User>().Add(new User
		{
			Username = "admin",
			Password = "admin",
		});
	}
}