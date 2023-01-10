using Microsoft.AspNetCore.Mvc;
using eventful_api_master.Models;
using eventful_api_master.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace eventful_api_master.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await Task.FromResult(_user.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await Task.FromResult(_user.GetUser(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        public async Task<IActionResult> Add(User user)
        {
            try
            {
                await _user.AddUser(user);
                return CreatedAtAction(nameof(Add), new { id = user.Id }, user);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                _user.UpdateUser(user);
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
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            await _user.DeleteUser(id);
            return NoContent();
        }
        private bool UserExists(int id)
        {
            return _user.CheckUser(id);
        }
    }
}
