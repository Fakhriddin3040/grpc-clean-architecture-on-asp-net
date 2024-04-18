using AuthMicroservice.Domain.Abstracts;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.Interfaces.Entities;
using AuthMicroservice.Domain.Interfaces.ValueObjects;
using AuthMicroservice.Domain.ValueObjects;

namespace AuthMicroservice.Domain.Entities;

public class User : BaseUser, IUser
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int? Age { get; set; }

    public Contacts Contacts { get; set; }

    public DateTime? Birthday { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }


}