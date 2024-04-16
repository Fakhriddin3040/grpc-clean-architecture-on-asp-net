using System.Security.Cryptography.X509Certificates;
using AuthMicroservice.Application.Common.Extensions;
using AuthMicroservice.Infrastructure.DTOs;
using AuthMicroservice.Domain.Entities;

using AuthMicroservice.Infrastructure.Interfaces.Services;
using AuthMicroservice.ProtoServices;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AuthMicroservice.Infrastructure.Controllers
{
    public class UserController : UserGrpcManager.UserGrpcManagerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public override async Task GetAll(
            Empty request,
            IServerStreamWriter<UserListDTOProto> responseStream,
            ServerCallContext context
            )
        {
            List<UserListDTO> users = _userService.GetAll().ToList();

            for (int i = 0; i < users.Count(); i++)
            {
                UserListDTOProto userProto = _mapper.Map<UserListDTOProto>(users[i]);
                await responseStream.WriteAsync(userProto);
            }
        }

        public override async Task<UserDetailDTOProto> GetDetail(
            UserLookup request,
            ServerCallContext context)
        {
            UserDetailDTO userDetail = await _userService.GetDetail(Guid.Parse(request.Id));

            return _mapper.Map<UserDetailDTOProto>(userDetail);
        }

        public override async Task<UserListDTOProto> UpdateMe(UserUpdateMeDTOProto request, ServerCallContext context)
        {
            Guid currentUserId = context.GetHttpContext().GetUserIdFromClaims();
            UserUpdateDTO userData = _mapper.Map<UserUpdateDTO>(request);
            UserListDTO updatedUser = await _userService.Update(currentUserId, userData);
            UserListDTOProto response = _mapper.Map<UserListDTOProto>(updatedUser);

            return response;
        }

        public override async Task<UserListDTOProto> Update(UserUpdateDTOProto request, ServerCallContext context)
        {
            UserUpdateDTO userData = _mapper.Map<UserUpdateDTO>(request);
            UserListDTO updatedUser = await _userService.Update(userData.Id, userData);
            UserListDTOProto response = _mapper.Map<UserListDTOProto>(updatedUser);

            return response;
        }

        public override async Task<UserListDTOProto> Create(UserCreateDTOProto request, ServerCallContext context)
        {
            UserCreateDTO newUser = _mapper.Map<UserCreateDTO>(request);
            UserListDTO createdUser = await _userService.Create(newUser);
            UserListDTOProto response = _mapper.Map<UserListDTOProto>((UserListDTO)createdUser);

            return response;
        }

        public override async Task<Empty> Delete(UserLookup request, ServerCallContext context)
        {
            await _userService.Delete(Guid.Parse(request.Id));
            return new Empty();
        }
    }
}