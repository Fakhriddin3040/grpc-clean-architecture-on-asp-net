using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;

namespace AuthMicroservice.Application.DTOs
{
    public class UserListDTO : IUserListDTO, IUser
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set;}
    }
}