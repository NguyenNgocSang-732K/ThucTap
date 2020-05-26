using Microsoft.AspNetCore.Http;
using System;

namespace Supports
{
    public class CookieCuaSang
    {
        public static void Set(HttpContext httpContext, string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = expireTime.HasValue ? DateTime.Now.AddMinutes(expireTime.Value) : DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Append(key, value, option);
        }

        public static void Remove(HttpContext httpContext, string key)
        {
            httpContext.Response.Cookies.Delete(key);
        }

        public static string Get(HttpContext httpContext, string key)
        {
            return httpContext.Request.Cookies[key].ToString();
        }
    }
}
