using Glossary.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Infrastructure.Repositories
{
    public class HttpContextCurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextCurrentUser(IHttpContextAccessor accessor) => _httpContextAccessor = accessor;

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public Guid UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var value = user?.FindFirstValue("sub")
                         ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(value, out var id) ? id : Guid.Empty;
            }
        }
    }
}
