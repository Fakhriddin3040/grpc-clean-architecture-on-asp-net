using AuthMicroservice.Domain.Interfaces.Entities;
using AuthMicroservice.Domain.Interfaces.ValueObjects;
using AuthMicroservice.Domain.ValueObjects;

namespace AuthMicroservice.Domain.Entities;

public class User : IGuid, IUser, IPerson
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    private string Password { get; set; }

    public string Role { get; set; }

    public bool IsActive { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int? Age { get; set; }

    public Contacts Contacts { get; set; }

    public DateOnly? Birthday { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}