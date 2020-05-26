using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Client.Models.Entities;

namespace Supports
{
    public class SercurityManagerCuaSang
    {
        public static void Login(HttpContext httpContext, Account account, string cheme)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(GetUserClaim(account), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            httpContext.SignInAsync(cheme, claimsPrincipal);
        }

        public static void Logout(HttpContext httpContext, string cheme)
        {
            httpContext.SignOutAsync(cheme);
        }

        private static IEnumerable<Claim> GetUserClaim(Account account)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("id", account.Id + ""));
            claims.Add(new Claim(ClaimTypes.Name, account.Name));
            claims.Add(new Claim(ClaimTypes.Email, account.Email));
            claims.Add(new Claim(ClaimTypes.Role, account.Role ? "1" : "0"));
            return claims;
        }
    }
}
