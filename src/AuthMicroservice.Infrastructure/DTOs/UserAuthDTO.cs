namespace AuthMicroservice.Infrastructure.DTOs
{
    public class UserAuthDTO
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
    }
}