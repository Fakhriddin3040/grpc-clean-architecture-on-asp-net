using AuthMicroservice.Domain.ValueObjects;

namespace AuthMicroservice.Domain.Interfaces.Entities
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int? Age { get; set; }
        DateOnly? Birthday { get; set; }
        Contacts Contacts { get; set; }
    }
}