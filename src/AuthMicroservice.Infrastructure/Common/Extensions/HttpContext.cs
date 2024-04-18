using System.Security.Claims;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Infrastructure.DTOs;

using AuthMicroservice.Application.Interfaces.Services;
using AuthMicroservice.Infrastructure.Common.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AuthMicroservice.Infrastructure.Services.Interfaces;

namespace AuthMicroservice.Application.Common.Extensions
{
    public static class HttpContextExtension
    {

        public static async Task<IUser> GetCurrentUserAsync(this HttpContext httpContext)
        {
            if(!httpContext.User.Identity!.IsAuthenticated)
            {
                throw new UnauthenticatedRpcException("User not authenticated");
            }

            return await GetUserAsync(httpContext, GetUserIdFromClaims(httpContext));
        }

        public static Guid GetUserIdFromClaims(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new UnauthenticatedRpcException("You need to authorize.");
            }

            return Guid.Parse(userId);
        }

        private static async Task<IUser> GetUserAsync(HttpContext httpContext, Guid userId)
        {
            var userService = httpContext.RequestServices.GetRequiredService<IUserService>();
            IMapper mapper = httpContext.RequestServices.GetRequiredService<IMapper>();

            UserDetailDTO userDetail = await userService.GetUserDetail(userId);
            IUser user = mapper.Map<IUser>(userDetail);

            return user;
        }
    }
}