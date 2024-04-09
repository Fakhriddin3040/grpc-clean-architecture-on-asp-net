using Grpc.Core;
using AutoMapper;
using AuthMicroservice.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Services;

namespace AuthMicroservice.Application;

public class JwtAuthGrpcService : AuthenticationService.AuthenticationServiceBase
{
    IUserService _userService;
    IPasswordService _passwordService;
    IMapper _mapper;
    IJwtService _jwtService;

    public JwtAuthGrpcService(
        IMapper mapper,
        IJwtService jwtService,
        IUserService userService,
        IPasswordService passwordService
        )
    {
        _mapper = mapper;
        _jwtService = jwtService;
        _userService = userService;
        _passwordService = passwordService;
    }

    public override async Task<AuthResponse> Login(AuthRequest request, ServerCallContext context)
    {
        IUserListDTO user = await _userService.GetByUsername(request.Username);

        if (user == null)
        {
            throw new RpcException(new Status(
                StatusCode.NotFound, "Неверное имя пользователя или пароль."
            ));
        }

        string hashedPassword = _passwordService.HashPassword(user.Password, user.Salt);

        bool correctAuth = _passwordService.VerifyPassword(user.Password, hashedPassword, user.Salt);

        if (!correctAuth)
        {
            throw new RpcException(new Status(
                StatusCode.NotFound, "Неверное имя пользователя или пароль."
            ));
        }

        return new AuthResponse
        {
            Token = _jwtService.GenerateAccessToken(user.Id)
        };
    }

    public override async Task<AuthResponse> Register(AuthRequest request, ServerCallContext context)
    {
        string salt = _passwordService.GenerateSalt();
        IUserCreateDTO userCreateDTO = new UserCreateDTO() {
            Username = request.Username,
            Password = _passwordService.HashPassword(request.Password, salt),
            Salt = salt,
            Age = 5
        };

        IUserListDTO createdUser = await _userService.Create(userCreateDTO);

        string token = _jwtService.GenerateAccessToken(createdUser.Id);

        return await Task.FromResult(
            new AuthResponse {
                Token = token
            }
        );
    }

    [Authorize]
    public override Task<ValidateResponse> IsValidToken(
        ValidateRequest request,
        ServerCallContext context)
        {
            return Task.FromResult(new ValidateResponse { IsValid = true });
        }
}