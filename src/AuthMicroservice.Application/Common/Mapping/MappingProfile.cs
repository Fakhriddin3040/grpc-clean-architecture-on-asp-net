using System.Reflection;
using System.Runtime;
using AuthMicroservice.Application.DTOs;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.ValueObjects;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

namespace AuthMicroservice.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyCreatedMappings();
        }

        private void ApplyCreatedMappings()
        {
            CreateMap<User, UserListDTO>()
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