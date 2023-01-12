using eventful_api_master.Context;
using eventful_api_master.Interface;
using eventful_api_master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eventful_api_master.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController: ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IUserRepository _userRepository;

        public TokenController(IConfiguration configuration, IAuthenticationRepository authenticationRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _authenticationRepository = authenticationRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Validate(UserLogin user)
        {
            if (user != null && user.Email != null && user.Password != null)
            {
                var currentUser = await GetUser(user.Email, user.Password);

                if (currentUser != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", currentUser.Id.ToString()),
                        new Claim("UserName", currentUser.FirstName.ToString()),
                        new Claim("Email", currentUser.Email.ToString()),
                    };

                    var token = CreateToken(claims);
                    var refreshToken = RefreshToken();

                    return Ok(new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo
                    });
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
                
        private async Task<User?> GetUser(string email, string password)
        {
            return await _authenticationRepository.SignIn(email, password);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], authClaims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: signIn);

            return token;
        }

        private static string RefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
