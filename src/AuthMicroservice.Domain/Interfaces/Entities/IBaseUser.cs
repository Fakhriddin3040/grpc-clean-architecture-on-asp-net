using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Domain.Interfaces.Entities
{
    public interface IBaseUser : IPassword
    {
        string Username { get; set; }

        bool? IsActive { get; set; }

        string Salt { get; set; }
    }
}