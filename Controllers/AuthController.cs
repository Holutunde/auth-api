using Auth.Dto;
using Auth.Helpers;
using Auth.Interfaces;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ValidationHelper.IsValidEmail(registerDto.Email))
            {
                return BadRequest("Invalid email format.");
            }

            if (!ValidationHelper.IsValidPassword(registerDto.Password))
            {
                return BadRequest("Password must be at least 7 characters long and contain at least one number and one special character.");
            }

            var userExist = await _userService.GetUserByEmail(registerDto.Email);
            if (userExist != null)
            {
                return Conflict("User with the provided email already exists.");
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            await _userService.Register(user);

            return CreatedAtRoute(new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.Login(loginDto);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _jwtTokenService.GenerateToken(user.Id, user.Email);
            return Ok(new { user, Token = token });
        }
    }
}
