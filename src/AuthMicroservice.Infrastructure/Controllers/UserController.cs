using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Services;
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
            List<IUserListDTO> users = _userService.GetAll().ToList();

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
            IUserDetailDTO userDetail = await _userService.GetDetail(Guid.Parse(request.Id));

            return _mapper.Map<UserDetailDTOProto>(userDetail);
        }

        // public override async Task<UserListDTOProto> Update(UserUpdateDTOProto request, ServerCallContext context)
        // {
        //     var userId = context.GetHttpContext().Cla;
        //     ;
        //     IUserUpdateDTO userData = _mapper.Map<IUserUpdateDTO>(user);

        // }
    }
}