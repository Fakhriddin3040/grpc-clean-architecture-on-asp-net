using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;

namespace AuthMicroservice.Application.DTOs
{
    public class UserListDTO : IUserListDTO
    {
        public Guid Id { get; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime CreatedAt { get; }

        public DateTime UpdatedAt { get; }
    }
}