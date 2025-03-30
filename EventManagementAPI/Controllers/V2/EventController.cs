using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Controllers.V2
{
    [ApiVersion("2.0")]
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

        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Versao = "v2", Mensagem = "Lista de eventos da versão 2 com novos recursos" });
        }
    }
}
