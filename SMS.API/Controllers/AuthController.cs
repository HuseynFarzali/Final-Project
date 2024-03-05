using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.Authentication.Models;
using SMS.Authentication.Services.Contracts;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAppAuthenticationService _authService;

        public AuthController(IAppAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(await _authService.GetCurrentUser());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            return Ok(await _authService.Register(registerRequestModel));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            return Ok(await _authService.Login(loginRequestModel));
        }
    }
}
