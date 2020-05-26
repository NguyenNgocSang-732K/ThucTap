using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.Models.Entities;

namespace Services.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountRestController : ControllerBase
    {
        private IConfiguration configuration;
        private IAccountRepository accountRepository;

        public AccountRestController(IAccountRepository _accountRepository, IConfiguration _configuration)
        {
            configuration = _configuration;
            accountRepository = _accountRepository;
        }

        [Authorize]
        [HttpGet("getdata/{page}/{rows}")]
        public async Task<IActionResult> GetData(int page, int rows)
        {
            try
            {
                return Ok(await accountRepository.GetData(page, rows));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Account account = await accountRepository.GetById(id);
                if (account == null) { return NotFound(); }
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Account account)
        {
            try
            {
                IActionResult response = Unauthorized();
                Account accountlogin = await accountRepository.Login(account);
                if (accountlogin != null)
                {
                    var jsonToken = GenerateJSONWebToken(accountlogin);
                    response = Ok(jsonToken);
                }
                return response;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            try
            {
                return Ok(await accountRepository.Create(account));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("modify")]
        public async Task<IActionResult> Modify([FromBody] Account account)
        {
            try
            {
                return Ok(await accountRepository.Modify(account));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                return Ok(await accountRepository.Remove(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        private string GenerateJSONWebToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id",account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,account.Name),
                new Claim(JwtRegisteredClaimNames.Email,account.Email),
                new Claim("role",account.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}