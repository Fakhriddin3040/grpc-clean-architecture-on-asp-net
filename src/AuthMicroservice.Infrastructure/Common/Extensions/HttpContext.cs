using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Domain.Interfaces.DTOs;
using AuthMicroservice.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AuthMicroservice.Application.Common.Extensions
{
    public static class HttpContextExtension
    {

        public static async Task<IUser> GetCurrentUserAsync(this HttpContext httpContext)
        {
            if(!httpContext.User.Identity!.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!;

            var userId = Guid.Parse(userIdClaim.Value);
            return await GetUserAsync(httpContext, userId);
        }

        private static async Task<IUser> GetUserAsync(HttpContext httpContext, Guid userId)
        {
            IUserService userService = httpContext.RequestServices.GetRequiredService<IUserService>();
            IMapper mapper = httpContext.RequestServices.GetRequiredService<IMapper>();

            IUserDetailDTO userDetail = await userService.GetDetail(userId);
            IUser user = mapper.Map<IUser>(userDetail);

            return user;
        }

        public static Guid GetUserIdFromClaims(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
            }
            return default;
        }
    }
}