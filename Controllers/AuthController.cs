using eventful_api_master.Interface;
using eventful_api_master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eventful_api_master.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("token")]
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
                        new Claim(ClaimTypes.Name, currentUser.Id.ToString()),
                        new Claim("name", currentUser.FirstName.ToString()),
                        new Claim("email", currentUser.Email.ToString()),
                    };

                    var token = CreateToken(claims);

                    return Ok(new
                    {
                        access_token = new JwtSecurityTokenHandler().WriteToken(token),
                        expires_in = token.ValidTo
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        code = "invalid_credentials",
                        msg = "Usuário ou senha inválidos"
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    code = "bad_request",
                    msg = "Ocorreu um erro inesperado"
                });
            }
        }
                
        private async Task<User?> GetUser(string email, string password)
        {
            return await _userRepository.Validate(email, password);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], authClaims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: signIn);

            return token;
        }
    }
}
