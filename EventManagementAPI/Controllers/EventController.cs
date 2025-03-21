using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.Include(e => e.Artists).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Events.Include(e => e.Artists).FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, @event);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEvent(int id, [FromBody] JsonPatchDocument<Event> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(@event, (error) => ModelState.AddModelError(error.AffectedObject?.ToString() ?? "", error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // Adicionar um artista a um evento
        [HttpPost("{eventId}/artists/{artistId}")]
        public async Task<IActionResult> AddArtistToEvent(int eventId, int artistId)
        {
            var evento = await _context.Events.FindAsync(eventId);
            var artista = await _context.Artists.FindAsync(artistId);

            if (evento == null || artista == null) return NotFound();

            if (!evento.Artists.Contains(artista))
            {
                evento.Artists.Add(artista);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        // Remover um artista de um evento
        [HttpDelete("{eventId}/artists/{artistId}")]
        public async Task<IActionResult> RemoveArtistFromEvent(int eventId, int artistId)
        {
            var evento = await _context.Events.Include(e => e.Artists).FirstOrDefaultAsync(e => e.Id == eventId);
            if (evento == null) return NotFound();

            var artista = evento.Artists.FirstOrDefault(a => a.Id == artistId);
            if (artista == null) return NotFound();

            evento.Artists.Remove(artista);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }

}
