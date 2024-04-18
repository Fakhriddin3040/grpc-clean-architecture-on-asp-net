using AuthMicroservice.Infrastructure.Common.Mapping;
using AutoMapper;

namespace AuthMicroservice.Infrastructure.DTOs
{
    public class UserAuthDTO: IMapFrom<UserDetailDTO>
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserDetailDTO, UserAuthDTO>()
                .ReverseMap();
        }
    }
}