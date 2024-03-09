using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace SparkNest.Common.Base.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string UserId
        {
            get
            {
                // Accessing the current user's identity
                var userId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value;
                return userId;
            }
        }
    }
}
