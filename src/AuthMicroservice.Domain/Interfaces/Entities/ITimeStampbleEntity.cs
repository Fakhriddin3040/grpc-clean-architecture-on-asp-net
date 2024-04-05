namespace AuthMicroservice.Domain.Interfaces.Entities
{
	public interface ITimeStampbleEntity
	{
		DateTime CreatedAt { get; }
		DateTime UpdatedAt { get; }
	}
}