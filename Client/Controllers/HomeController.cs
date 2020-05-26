using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Client.Models.Entities;
using Client.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Supports;

namespace Client.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private IAccountRepository accountRepository;
        private IBallotRequestRepository ballotRequestRepository;

        public HomeController(IAccountRepository _accountRepository, IBallotRequestRepository _ballotRequestRepository)
        {
            accountRepository = _accountRepository;
            ballotRequestRepository = _ballotRequestRepository;
        }

        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(Account account)
        {
            return Json(LoginCookie(account) ? "200" : "500");
        }

        [HttpGet("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet("createballot")]
        public IActionResult CreateBallot()
        {
            return View();
        }

        [HttpGet("listrequest")]
        public IActionResult ListRequest()
        {
            ViewBag.List = ballotRequestRepository.GetByIdUser(Convert.ToInt32(User.FindFirst("id").Value), CookieCuaSang.Get(HttpContext, "tokenuser"));
            return View();
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var request = ballotRequestRepository.GetById(id, CookieCuaSang.Get(HttpContext, "tokenuser"));
            return View(request);
        }


        [HttpPost("updaterequest")]
        public IActionResult Modify(BallotRequest ballotRequest)
        {
            ballotRequest.Id = ballotRequest.Id;
            ballotRequest.AccId = Convert.ToInt32(User.FindFirst("id").Value);
            ballotRequest.Status = 0;
            int rs = ballotRequestRepository.Modify(ballotRequest, CookieCuaSang.Get(HttpContext, "tokenuser"));
            return Json(rs > 0 ? "200" : "500");
        }

        [HttpPost("createballot")]
        public IActionResult CreateBallot(BallotRequest ballotRequest)
        {
            ballotRequest.AccId = Convert.ToInt32(User.FindFirst("id").Value);
            ballotRequest.Status = 0;
            int rs = ballotRequestRepository.Create(ballotRequest, CookieCuaSang.Get(HttpContext, "tokenuser"));
            return Json(rs > 0 ? "200" : "500");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            SercurityManagerCuaSang.Logout(HttpContext, "SCHEME_USER");
            return RedirectToAction("login", "home");
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
                CookieCuaSang.Set(HttpContext, "tokenuser", result, null);
                SercurityManagerCuaSang.Login(HttpContext, accountlogin, "SCHEME_USER");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}