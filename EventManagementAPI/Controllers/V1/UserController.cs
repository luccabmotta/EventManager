using EventManagementAPI.Interfaces;
using EventManagementAPI.Services;
using EventManagementAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Você acessou um endpoint protegido!" });
        }

        [MapToApiVersion("1.0")]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login loginRequest)
        {
            var token = _userService.Login(loginRequest.Username, loginRequest.Password);

            if (token == null)
                return Unauthorized(new { message = "Usuário ou senha inválidos" });

            return Ok(new { token });
        }
    }
}
