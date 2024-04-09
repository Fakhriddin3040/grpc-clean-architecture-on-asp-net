using AuthMicroservice.Domain.ValueObjects;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
    public interface IPersonDTO : IContactsDTO
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int? Age { get; set; }
        DateOnly? Birthday { get; set; }
    }
}