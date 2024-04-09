using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Entities;
using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Domain.Interfaces.DTOs
{
    public interface IUserListDTO : IGuid, ISalt
    {
        string Username { get; set; }

        string FirstName { get; set; }

        string Salt { get; set; }

        string Password { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string Phone { get; set; }

        DateTime? CreatedAt { get; }

        DateTime? UpdatedAt { get; }
    }
}