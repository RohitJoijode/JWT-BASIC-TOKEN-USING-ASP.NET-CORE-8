using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_BASIC_TOKEN_DOT_NET_CORE_8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly Jwtsettings _jwtSettings;

        public AuthController(Jwtsettings jwtsettings)
        {
            _jwtSettings = jwtsettings;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"testuser"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("082F41538C1178DE768A9AC86291678D"));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer : "yourIssuer",
                audience : "yourAudience",
                claims : claims,
                expires : DateTime.Now.AddMinutes(int.Parse("2")),
                signingCredentials : creds
          );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
