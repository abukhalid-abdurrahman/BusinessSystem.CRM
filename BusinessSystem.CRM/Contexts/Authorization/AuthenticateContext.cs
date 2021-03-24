using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessSystem.Database.Models;
using BusinessSystem.Database.Models.BusinessObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace BusinessSystem.CRM.Contexts.Authorization
{
    public class AuthenticateContext
    {
        private readonly HttpContext _httpContext;

        public AuthenticateContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task SignIn(UserEntityModel userEntityModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userEntityModel.Login),
                new Claim("UserId", userEntityModel.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userEntityModel.RoleId.ToString())
            };

            var claimsId = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsId);
            var properties = new AuthenticationProperties() { IsPersistent = true };
            await _httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                properties
            );
        }

        public async Task SignOut()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}