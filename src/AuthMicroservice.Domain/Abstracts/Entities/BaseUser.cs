using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Abstracts;

public abstract class BaseUser : IBaseUser
{
    public string Username { get; set; }

    public string Salt { get; set; }

    public string Password { get; set; }

    public bool? IsActive { get; set; }

    public string Role { get; set; }
}
