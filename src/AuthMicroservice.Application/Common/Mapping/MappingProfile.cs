using System.Reflection;
using AuthMicroservice.Application.DTOs;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.ValueObjects;
using AuthMicroservice.ProtoServices;
using AutoMapper;

namespace AuthMicroservice.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyUsersDTOsMapping();
            ApplyUsersGrpcDTOsMapping();
        }

        private void ApplyUsersDTOsMapping()
        {
            CreateMap<User, UserListDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contacts.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contacts.Phone));

            CreateMap<User, UserDetailDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contacts.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contacts.Phone));

            CreateMap<User, UserCreateDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contacts.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contacts.Phone));

            CreateMap<User, UserUpdateDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contacts.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contacts.Phone));

            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));

            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));

            CreateMap<UserListDTO, User>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));

            CreateMap<UserDetailDTO, User>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));
        }

        private void ApplyUsersGrpcDTOsMapping()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AllowNullCollections = true;
            });
            CreateMap<User, UserProto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contacts.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contacts.Phone));

            CreateMap<UserProto, User>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));

            CreateMap<UserListDTO, UserListDTOProto>();

            CreateMap<UserDetailDTO, UserDetailDTOProto>();

            CreateMap<UserCreateDTO, UserCreateDTOProto>();

            CreateMap<UserUpdateDTO, UserUpdateDTOProto>();

            CreateMap<UserListDTOProto, UserListDTO>();

            CreateMap<UserDetailDTOProto, UserDetailDTO>();

            CreateMap<UserUpdateDTOProto, UserUpdateDTO>();

            CreateMap<UserCreateDTOProto, UserCreateDTO>();

            // CreateMap<User, UserUpdateDTOProto>()
            //     .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contacts.Email))
            //     .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contacts.Phone));

            // CreateMap<UserProto, User>()
            //     .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));
                
            // CreateMap<UserCreateDTOProto, User>()
            //     .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));

            // CreateMap<UserUpdateDTOProto, User>()
            //     .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));

            // CreateMap<UserListDTOProto, User>()
            //     .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new Contacts(src.Email, src.Phone)));
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromTypes = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromTypes;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] {typeof(Profile)};

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName, argumentTypes);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] {this});
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, new object[] {this});
                        }
                    }
                }
            }
        }
    }
}