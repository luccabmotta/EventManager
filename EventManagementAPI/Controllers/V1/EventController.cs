using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Services;
using EventManagementAPI.Services.EventManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace EventManagementAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableRateLimiting("fixed")] // Aplica Rate Limiting
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sort = null,
            [FromQuery] string? name = null,
            [FromQuery] string? location = null)
        {
            var query = _eventService.GetAll();

            // Filtros
            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));

            query = sort?.ToLower() switch
            {
                "name_asc" => query.OrderBy(e => e.Name),
                "name_desc" => query.OrderByDescending(e => e.Name),
                "date_asc" => query.OrderBy(e => e.Date),
                "date_desc" => query.OrderByDescending(e => e.Date),
                _ => query.OrderBy(e => e.Id) 
            };

            pageSize = pageSize > 200 ? 200 : pageSize;

            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var response = new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };

            return Ok(response);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var evento = await _eventService.GetByIdAsync(id);
            if (evento == null) return NotFound();
            return Ok(evento);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            await _eventService.AddAsync(@event);
            return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, @event);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            var success = await _eventService.UpdateAsync(id, @event);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEvent(int id, [FromBody] JObject eventPatch)
        {
            var success = await _eventService.PatchEventAsync(id, eventPatch);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteAsync(id);
            return NoContent();
        }
    }
}
