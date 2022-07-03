using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebApi.Models;
using CMPaymentControlMonitor.WebInterface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace CMPaymentControlMonitor.WebApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticationController : Controller
    {
        private IConfiguration _config;
        private IUserRepository _UserRepository;
        private PasswordHasher<IdentityUser> PasswordHasher;

        public AuthenticationController(IConfiguration config, IUserRepository userRepository)
        {
            _UserRepository = userRepository;
            _config = config;
            PasswordHasher = new PasswordHasher<IdentityUser>();
        }

        /// <summary>
        /// Request to POST a login
        /// </summary>
        /// <param name="login">Specify login info.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Authenticate([FromBody]ApiLoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = BuildToken(user.UserName);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string BuildToken(string username)
        {
            var claims = new[]
            {
                new Claim("Username", username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(14),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IdentityUser AuthenticateUser(ApiLoginModel login)        
        {
            // For testing purposes
            if (login.Username == "test" && login.Password == "Testing123")
            {
                return new IdentityUser
                {
                    UserName = login.Username
                };
            }

            // Checks if there's a user with the specified username
            var user = _UserRepository.GetUser(login.Username);

            // If user is found and correct password is correct returns the user
            if (user != null &&
                PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password) ==
                PasswordVerificationResult.Success)
                    return user;

            return null;
        }
    }
}