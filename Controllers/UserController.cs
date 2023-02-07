using Microsoft.AspNetCore.Mvc;
using eventful_api_master.Models;
using eventful_api_master.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Data;

namespace eventful_api_master.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public UserController(IConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await Task.FromResult(_userRepository.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> Get(int id)
        {
            var user = await Task.FromResult(await _userRepository.GetUser(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(User user)
        {            
            try
            {
                user.HashPassword(_configuration.GetValue<string>("PasswordHash"));
                await _userRepository.AddUser(user);

                return CreatedAtAction(nameof(Add), new { id = user.Id }, user);
            }
            catch (BadHttpRequestException)
            {
                return BadRequest(new
                {
                    code = "generic_error",
                    msg = "Não foi possivel cadastrar o usuario, verifique os campos informados"
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Update(int id, [FromBody]JsonPatchDocument<User> user)
        {
            var userUpdate = await _userRepository.GetUser(id);

            ClaimsIdentity? claimsIdentity = this.User.Identity as ClaimsIdentity;
            int.TryParse(claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value, out int userId);

            if (userUpdate == null && id != userId)
            {                
                return NotFound();
            }
            try
            {
                user.ApplyTo(userUpdate,ModelState);
                await _userRepository.UpdateUser(userUpdate);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(userUpdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Delete(int id)
        {
            try
            {
                User? user = await _userRepository.GetUser(id);

                ClaimsIdentity? claimsIdentity = this.User.Identity as ClaimsIdentity;
                int.TryParse(claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value, out int userId);

                if (user == null || id == userId)
                {
                    return NotFound();
                }

                user.Active = false;
                user.ChangeUser = userId;
                user.ChangeDate = DateTime.Now;
                await _userRepository.DeleteUser(user);
            }
            catch (DBConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }            
            return NoContent();
        }
        private bool UserExists(int id)
        {
            return _userRepository.CheckUser(id);
        }
    }
}
