using AuthMicroservice.Domain.Abstracts.Fields;
using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Abstracts;

public abstract class BaseUser : SaltGenerator, IBaseUser
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public bool? IsActive { get; set; }

    public string Role { get; set; }
}
