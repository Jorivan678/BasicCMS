using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace webapi.Application.Tools.Implementations
{
    internal class AuthValidator : IAuthValidator
    {
        private readonly IHttpContextAccessor _contextAccesor;

        public AuthValidator(IHttpContextAccessor contextAccesor)
        {
            _contextAccesor = contextAccesor;
        }

        public bool HasId(string id)
        {
            if (_contextAccesor.HttpContext is null) return false;

            return _contextAccesor.HttpContext.User.HasId(id);
        }

        public bool HasId<T>(T id) where T : struct
        {
            if (_contextAccesor.HttpContext is null) return false;

            return _contextAccesor.HttpContext.User.HasId(id);
        }

        public bool HasRole(string roleName)
        {
            if (_contextAccesor.HttpContext is null) return false;

            return _contextAccesor.HttpContext.User.IsInRole(roleName);
        }
    }
}
