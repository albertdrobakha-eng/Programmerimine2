using System.Linq;
using System.Security.Claims;
using KooliProjekt.Application.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KooliProjekt.WebAPI
{
    public class WebUserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public WebUserProvider(IHttpContextAccessor accessor) { _accessor = accessor; }

        public int? UserId()
        {
            var claim = _accessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim != null && int.TryParse(claim.Value, out var id) ? id : (int?)null;
        }
    }
}
