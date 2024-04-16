

namespace AuthMicroservice.Infrastructure.DTOs
{
    public class UserUpdateDTO
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        

        public int? Age { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime? Birthday { get; set; }
    }
}