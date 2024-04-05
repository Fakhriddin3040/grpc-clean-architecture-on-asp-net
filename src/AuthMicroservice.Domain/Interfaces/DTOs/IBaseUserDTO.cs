namespace AuthMicroservice.Domain.Interfaces.DTOs
{
	public interface IBaseUserDTO
	{
		string Username { get; }
		string Password { get; }
		string Role { get; }
	}
}