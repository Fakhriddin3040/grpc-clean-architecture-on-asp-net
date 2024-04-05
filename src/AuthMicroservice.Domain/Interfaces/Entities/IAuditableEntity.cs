using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Entities
{
	public interface IAuditableEntity
	{
		IBaseUser CreatedBy{ get; }
		IBaseUser UpdatedBy{ get; }
	}
}