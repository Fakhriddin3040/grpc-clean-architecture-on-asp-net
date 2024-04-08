namespace AuthMicroservice.Domain.Interfaces.Entities
{
	public interface IBaseUser
	{
		string Username { get; }
		string Role { get; }
		bool IsActive { get; }
	}
}