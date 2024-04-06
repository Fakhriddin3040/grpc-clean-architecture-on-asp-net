using AuthMicroservice.Protos;
using Microsoft.IdentityModel.Tokens;
using Grpc.Core;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.Services;
using AuthMicroservice.Domain.Configurations;
using AuthMicroservice.Domain.Interfaces.DTOs;

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
        IUser user = new User() {
            Username = request.Username,
            Password = request.Password,
        };
        _userService.Create(user);
        return Task.FromResult(new SuccessJwtLoginResponse
        {
            Token = ""
        });
    }

    public override Task<SuccessJwtLoginResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        IQueryable<IUserListDTO> users = _userService.GetAll();

        var token = GetAccessToken(Guid.NewGuid());

        Console.WriteLine(users);

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
			issuer: JwtAuthOptions.ISSUER,
			audience: JwtAuthOptions.AUDIENCE,
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