using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagementAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableRateLimiting("fixed")] // Aplica Rate Limiting
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sort = null,
            [FromQuery] string? name = null,
            [FromQuery] string? email = null)
        {
            var query = _artistService.GetAll();

            // Filtros
            if (!string.IsNullOrEmpty(name))
                query = query.Where(a => a.Name.Contains(name));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(a => a.Email.Contains(email));

            // Ordenação
            query = sort?.ToLower() switch
            {
                "name_asc" => query.OrderBy(a => a.Name),
                "name_desc" => query.OrderByDescending(a => a.Name),
                "email_asc" => query.OrderBy(a => a.Email),
                "email_desc" => query.OrderByDescending(a => a.Email),
                _ => query.OrderBy(a => a.Id) // Padrão
            };

            pageSize = pageSize > 200 ? 200 : pageSize;

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
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _artistService.GetArtistByIdAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            var createdArtist = await _artistService.CreateArtistAsync(artist);

            return CreatedAtAction(nameof(GetArtist), new { id = createdArtist.Id }, createdArtist);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            var success = await _artistService.UpdateArtistAsync(id, artist);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchArtist(int id, [FromBody] JObject artistPatch)
        {
            var success = await _artistService.PatchArtistAsync(id, artistPatch);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var success = await _artistService.DeleteArtistAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
