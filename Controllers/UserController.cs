﻿using Microsoft.AspNetCore.Mvc;
using eventful_api_master.Models;
using eventful_api_master.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace eventful_api_master.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await Task.FromResult(_userRepository.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await Task.FromResult(await _userRepository.GetUser(id));
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
                await _userRepository.AddUser(user);
                return CreatedAtAction(nameof(Add), new { id = user.Id }, user);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody]JsonPatchDocument<User> user)
        {
            var userUpdate = await _userRepository.GetUser(id);

            if (userUpdate == null)
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
        public async Task<ActionResult<User>> Delete(int id)
        {
            await _userRepository.DeleteUser(id);
            return NoContent();
        }
        private bool UserExists(int id)
        {
            return _userRepository.CheckUser(id);
        }
    }
}
