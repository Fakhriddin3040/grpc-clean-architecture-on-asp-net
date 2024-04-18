using AuthMicroservice.Domain.Interfaces.Entities;
using AuthMicroservice.Domain.Interfaces.Fields;

namespace AuthMicroservice.Domain.Entities
{
    public interface IUser : IBaseEntity, IBaseUser
    {
    }
}