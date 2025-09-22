using Glossary.API.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Glossary.API.Controllers
{
    public class AuthController : Controller
    {

        private readonly IConfiguration _config;
        public AuthController(IConfiguration config) => _config = config;


        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest req)
        {
            if (req.Username != "author" || req.Password != "Passw0rd!")
                return Unauthorized();

            var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, req.Username),
            new Claim(ClaimTypes.Role, "Author")
        };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { access_token = tokenString });
        }
    }
}
