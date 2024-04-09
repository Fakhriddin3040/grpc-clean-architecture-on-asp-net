using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Domain.Interfaces.Entities
{
    public abstract class BaseEntity : IGuid
    {
        public Guid Id { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}