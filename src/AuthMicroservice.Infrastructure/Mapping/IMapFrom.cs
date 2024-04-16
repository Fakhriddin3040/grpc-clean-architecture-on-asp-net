using AutoMapper;

namespace AuthMicroservice.Infrastructure.Common.Mapping
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}