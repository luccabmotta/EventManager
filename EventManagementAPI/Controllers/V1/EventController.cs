using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

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

            // Ordenação
            query = sort?.ToLower() switch
            {
                "name_asc" => query.OrderBy(e => e.Name),
                "name_desc" => query.OrderByDescending(e => e.Name),
                "date_asc" => query.OrderBy(e => e.Date),
                "date_desc" => query.OrderByDescending(e => e.Date),
                _ => query.OrderBy(e => e.Id) // Padrão
            };

            // Paginação
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
            if (id != @event.Id) return BadRequest();
            await _eventService.UpdateAsync(@event);
            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEvent(int id, [FromBody] JsonPatchDocument<Event> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var evento = await _eventService.GetByIdAsync(id);
            if (evento == null) return NotFound();

            patchDoc.ApplyTo(evento, (error) => ModelState.AddModelError(error.AffectedObject?.ToString() ?? "", error.ErrorMessage));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _eventService.UpdateAsync(evento);
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
