using AuthMicroservice.Domain.Abstracts.Fields;

namespace AuthMicroservice.Infrastructure.DTOs
{
    public class UserListDTO
    {
        public Guid Id { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime? CreatedAt { get; }

        public DateTime? UpdatedAt { get; }
    }
}