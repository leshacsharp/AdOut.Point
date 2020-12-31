using AdOut.Extensions.Authorization;
using AdOut.Point.Model.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace AdOut.Point.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypeNames.UserId)?.Value;
        }
    }
}
