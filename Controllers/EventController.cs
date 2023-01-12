using Microsoft.AspNetCore.Mvc;
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        public async Task<IActionResult> Add(Event eventData)
        {
            try
            {
                await _eventRepository.AddEvent(eventData);
                return CreatedAtAction(nameof(Add), new { id = eventData.Id }, eventData);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Event>> Update(int id, [FromBody]JsonPatchDocument<Event> eventData)
        {
            var eventUpdate = await _eventRepository.GetEvent(id);

            if (eventUpdate == null)
            {
                return NotFound();
            }
            try
            {
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
        public async Task<ActionResult<Event>> Delete(int id)
        {
            await _eventRepository.DeleteEvent(id);
            return NoContent();
        }
        private bool EventExists(int id)
        {
            return _eventRepository.CheckEvent(id);
        }
    }
}
