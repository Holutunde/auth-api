using Auth.Dto;
using Auth.Interfaces;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;


namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(IUserService userService, JwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var user = _userService.Register(registerDto);
            if (user == null)
            {
                return BadRequest("User registration failed");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.Login(loginDto);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _jwtTokenService.GenerateToken(user.Id, user.Email);
            return Ok(new { Token = token });
        }
    }
}
