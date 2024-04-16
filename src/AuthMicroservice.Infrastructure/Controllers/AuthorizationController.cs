using Grpc.Core;
using AutoMapper;
using AuthMicroservice.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;

using AuthMicroservice.Infrastructure.Interfaces.Services;
using Services.Authentication;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FluentValidation;

namespace AuthMicroservice.Infrastructure.Controllers;

public class AuthorizationController : AuthenticationGrpcService.AuthenticationGrpcServiceBase
{
    IUserService _userService;
    IPasswordService _passwordService;
    IMapper _mapper;
    IJwtService _jwtService;

    public AuthorizationController(
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
        var user = await _userService.AuthenticateUser(request.Username, request.Password);

        var token = _jwtService.GenerateAccessToken(user.Id);

        return await Task.FromResult( new 
        AuthResponse {
            Token = token
        });
    }

    public override async Task<AuthResponse> Register(AuthRequest request, ServerCallContext context)
    {
        string salt = _passwordService.GenerateSalt();
        UserCreateDTO userCreateDTO = new UserCreateDTO() {
            Username = request.Username,
            Password = _passwordService.HashPassword(request.Password, salt),
            Age = 5
        };

        UserListDTO createdUser = await _userService.Create(userCreateDTO);

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