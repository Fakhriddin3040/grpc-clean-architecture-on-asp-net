using System.Text;
using AuthMicroservice.Protos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Services;
using AuthMicroservice.Domain.Configurations;
using System.Security.Cryptography;
using AuthMicroservice.Domain.ValueObjects;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Application.DTOs;

namespace AuthMicroservice.Application;

public class JwtAuthService: JwtAuthProtoService.JwtAuthProtoServiceBase
{
    IUserService _userService;

    public JwtAuthService(IUserService userService)
    {
        _userService = userService;
    }

    public override Task<SuccessJwtLoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        return Task.FromResult(new SuccessJwtLoginResponse
        {
            Token = "YouRToken"
        });
    }

    public override Task<SuccessJwtLoginResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
        };
        IUserListDTO newUser = _userService.Create(user);

        var token = GetAccessToken(user.Id);

        return Task.FromResult(new SuccessJwtLoginResponse
        {
            Token = token.Result.ToString()
        });
    }

    private async Task<SigningCredentials> GetSigningCredentials()
    {
        return await Task.FromResult(
            new SigningCredentials(
                JwtAuthOptions.GetSymmetricSecurityKey(), 
                SecurityAlgorithms.HmacSha256)
                );
    }

    private async Task<string> WriteJwtAccessToken(JwtSecurityToken jwtSecurityToken)
    {
        return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
    }

    public async Task<string> GetAccessToken(Guid id)
    {
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, id.ToString())
		};
		return await WriteJwtAccessToken(new JwtSecurityToken(
			issuer: JwtAuthOptions.Issuer,
			audience: JwtAuthOptions.Audience,
			claims: claims,
			expires: JwtAuthOptions.GetExpireTime(),
			signingCredentials: GetSigningCredentials().Result
		));
	}

    [Authorize]
    public override Task<ValidateTokenResponse> IsValidToken(
        ValidateTokenRequest request,
        ServerCallContext context)
        {
            return Task.FromResult(new ValidateTokenResponse
            {
                Valid = true,
            });
        }
}