using Microsoft.AspNetCore.Mvc;
using eventful_api_master.Models;
using eventful_api_master.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using eventful_api_master.Repository;
using System.Data;

namespace eventful_api_master.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController: ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            return await Task.FromResult(_eventRepository.GetEvents());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var eventData = await Task.FromResult(await _eventRepository.GetEvent(id));
            if (eventData == null)
            {
                return NotFound();
            }
            return Ok(eventData);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Add(Event eventData)
        {
            try
            {
                ClaimsIdentity? claimsIdentity = this.User.Identity as ClaimsIdentity;
                int.TryParse(claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value, out int userId);
                eventData.CreationUser = userId;

                await _eventRepository.AddEvent(eventData);
                return CreatedAtAction(nameof(Add), new { id = eventData.Id }, eventData);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<Event>> Update(int id, [FromBody]JsonPatchDocument<Event> eventData)
        {
            var eventUpdate = await _eventRepository.GetEvent(id);

            if (eventUpdate == null)
            {
                return NotFound();
            }
            try
            {
                ClaimsIdentity? claimsIdentity = this.User.Identity as ClaimsIdentity;
                int.TryParse(claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value, out int userId);
                eventUpdate.ChangeUser = userId;
                eventUpdate.ChangeDate = DateTime.Now;

                eventData.ApplyTo(eventUpdate, ModelState);
                await _eventRepository.UpdateEvent(eventUpdate);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(eventUpdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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
        public async Task<ActionResult<Event>> Delete(int id)
        {
            try
            {
                Event? eventData = await _eventRepository.GetEvent(id);

                ClaimsIdentity? claimsIdentity = this.User.Identity as ClaimsIdentity;
                int.TryParse(claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value, out int userId);

                if (eventData == null)
                {
                    return NotFound();
                }

                eventData.Active = false;
                eventData.ChangeUser = userId;
                eventData.ChangeDate = DateTime.Now;
                await _eventRepository.DeleteEvent(eventData);
            }
            catch (DBConcurrencyException)
            {
                if (!EventExists(id))
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
        private bool EventExists(int id)
        {
            return _eventRepository.CheckEvent(id);
        }
    }
}
