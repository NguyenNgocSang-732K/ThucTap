using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Client.Models.Entities;
using Client.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supports;

namespace Client.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/home")]
    public class HomeController : Controller
    {
        private IAccountRepository accountRepository;
        public HomeController(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }

        [Authorize(Roles = "1", AuthenticationSchemes = "SCHEME_ADMIN")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("~/")]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            SercurityManagerCuaSang.Logout(HttpContext, "SCHEME_ADMIN");
            return RedirectToAction("login", "home", new { area = "admin" });
        }

        [HttpPost("login")]
        public IActionResult Login(Account account)
        {
            return Json(LoginCookie(account) ? "200" : "500");
        }

        private bool LoginCookie(Account account)
        {
            try
            {
                string result = accountRepository.Login(account);
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = (JwtSecurityToken)handler.ReadToken(result);
                Account accountlogin = new Account
                {
                    Id = Convert.ToInt32(jsonToken.Claims.First(c => c.Type == "id").Value),
                    Name = jsonToken.Claims.First(c => c.Type == "sub").Value,
                    Email = jsonToken.Claims.First(c => c.Type == "email").Value,
                    Role = Convert.ToBoolean(jsonToken.Claims.First(c => c.Type == "role").Value)
                };
                CookieCuaSang.Set(HttpContext, "token", result, null);
                SercurityManagerCuaSang.Login(HttpContext, accountlogin, "SCHEME_ADMIN");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}