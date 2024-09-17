using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service.VNPAY
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? IpAddress { get; }
    }
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string? UserId => httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? IpAddress => httpContextAccessor?.HttpContext?.Connection?.LocalIpAddress?.ToString();
    }
}
