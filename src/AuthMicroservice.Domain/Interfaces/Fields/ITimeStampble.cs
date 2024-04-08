namespace AuthMicroservice.Domain.Interfaces.Fields
{
	public interface ITimeStampble
	{
		DateTime? CreatedAt { get; }
		DateTime? UpdatedAt { get; }
	}
}