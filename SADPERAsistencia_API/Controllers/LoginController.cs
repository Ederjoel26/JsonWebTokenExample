using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SADPERAsistencia_API.Models;
using SADPERAsistencia_API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace SADPERAsistencia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private cls_Encriptar _encriptar;

        public LoginController(IConfiguration config, cls_Encriptar encriptar)
        {
            _config = config;
            _encriptar = encriptar;
        }

        [AllowAnonymous]
        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody] mdl_Jwt Login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(Login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(mdl_Jwt Login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("fullName", _encriptar.ComputeSHA256Hash(Login.sNombre)),
                new Claim("email", _encriptar.ComputeSHA256Hash(Login.sEmail))
            };

            var token = new JwtSecurityToken(
              issuer: null,
              audience: null,
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private mdl_Jwt AuthenticateUser(mdl_Jwt login)
        {
            mdl_Jwt user = null;

            if (login.sNombre == _config["Jwt:SecretName"] && login.sEmail == _config["Jwt:SecretEmail"])
            {
                user = login;
            }

            return user;
        }
    }
}
