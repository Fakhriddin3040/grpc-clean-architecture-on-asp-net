using Microsoft.IdentityModel.Tokens;
using Grpc.Core;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AuthMicroservice.Domain.Interfaces.Services;
using AuthMicroservice.Domain.Configurations;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.ProtoServices;
using AutoMapper;
using AuthMicroservice.Application.DTOs;
using AuthMicroservice.Domain.Entities;

namespace AuthMicroservice.Application;

public class JwTokenAuthenticationGrpcService: JwtAuthGrpcService.JwtAuthGrpcServiceBase
{
    IUserService _userService;
    IMapper _mapper;

    public JwTokenAuthenticationGrpcService(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        IUserListDTO user = await _userService.GetByUsername(request.Username);

        if (user == null)
        {
            throw new RpcException(new Status(
                StatusCode.NotFound, "Неверное имя пользователя или пароль."
            ));
        }

        string hashedPassword = _userService.HashPassword(user.Password, user.Salt);

        bool correctAuth = _userService.VerifyPassword(user.Password, hashedPassword, user.Salt);

        if (!correctAuth)
        {
            throw new RpcException(new Status(
                StatusCode.NotFound, "Неверное имя пользователя или пароль."
            ));
        }

        return new LoginResponse
        {
            Token = GetAccessToken(user.Id)
        };
    }

    public override async Task<LoginResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        string salt = _userService.GenerateSalt();
        IUserCreateDTO userCreateDTO = new UserCreateDTO() {
            Username = request.Username,
            Password = _userService.HashPassword(request.Password, salt),
            Salt = salt,
            Age = 5
        };

        IUserListDTO _ = await _userService.Create(userCreateDTO);

        return await Task.FromResult(
            new LoginResponse {
                Token = "Token"
            }
        );
    }

    private SigningCredentials GetSigningCredentials()
    {
        return new SigningCredentials(
                JwtAuthOptions.GetSymmetricSecurityKey(), 
                JwtAuthOptions.SecurityAlgorithm
            );
    }

    private string WriteJwtAccessToken(JwtSecurityToken jwtSecurityToken)
    {
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public string GetAccessToken(Guid id)
    {
		return WriteJwtAccessToken(new JwtSecurityToken(
			issuer: JwtAuthOptions.ISSUER,
			audience: JwtAuthOptions.AUDIENCE,
			claims: JwtAuthOptions.GetClaims(id),
			expires: JwtAuthOptions.GetExpireTime(),
			signingCredentials: GetSigningCredentials()
		));
	}

    [Authorize]
    public override Task<ValidateResponse> IsValidToken(
        ValidateRequest request,
        ServerCallContext context)
        {
            return Task.FromResult(new ValidateResponse
            {
                Valid = true,
            });
        }
}