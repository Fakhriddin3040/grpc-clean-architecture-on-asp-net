using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Fields
{
	public interface IAuditableEntity
	{
		IBaseUser CreatedBy{ get; }
		IBaseUser UpdatedBy{ get; }
	}
}