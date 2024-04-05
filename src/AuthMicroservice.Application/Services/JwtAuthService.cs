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

namespace AuthMicroservice.Application;

public class JwtAuthService: JwtAuthProtoService.JwtAuthProtoServiceBase
{
	private readonly IConfiguration _config;
	private readonly string key;
	private readonly SecurityKey securityKey;
	IUserService _userService;

	public JwtAuthService(
		IConfiguration configuration,
		IUserService userService
		)
	{
		_config = configuration;
		key = _config["Jwt:Key"]!;
		securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
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
		var credentials = new SigningCredentials(
			securityKey, SecurityAlgorithms.HmacSha256);

		var jwtSecToken = new JwtSecurityToken(
		_config["Jwt:Issuer"],
		_config["Jwt:Issuer"],
		
		expires: DateTime.Now.AddMinutes(120),
		signingCredentials: credentials);

		var token = new JwtSecurityTokenHandler().WriteToken(jwtSecToken);
		return Task.FromResult(new SuccessJwtLoginResponse
		{
			Token = token
		});
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