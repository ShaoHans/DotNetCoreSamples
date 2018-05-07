using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Jwt认证与授权.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jwt认证与授权.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private JwtSettings _jwtSettings;

        public TestController(IOptions<JwtSettings> jwtSettingsAccesser)
        {
            _jwtSettings = jwtSettingsAccesser.Value;
        }

        [HttpGet, Route("[action]")]
        public IActionResult Login(string name,string password)
        {
            if (!("shz".Equals(name, StringComparison.OrdinalIgnoreCase) &&
                "123456".Equals(password, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,"shz"),
                new Claim(ClaimTypes.Role,"admin")
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: 
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                    SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(new { token });
        }

        [HttpGet,Route("[action]")]
        public IActionResult Access(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string result = httpClient.GetStringAsync("http://localhost:52372/api/values").Result;
            return Ok(new { result });
        }
    }
}
