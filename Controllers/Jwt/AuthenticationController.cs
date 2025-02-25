using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuggestionBoxApi.Data;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Models;
using SuggestionBoxApi.Services;


namespace SuggestionBoxApi.Controllers.Jwt
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly SuggboxContext _context;
        private readonly IMapper _mapper;

        public AuthenticationController(JwtService jwtService, SuggboxContext context, IMapper mapper)
        {
            _jwtService = jwtService;
            _context = context;
            _mapper = mapper;
        }

        //Registering a user
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] CreateUserDto user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("User with this email already exists");
            }

            user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);
            user.CreatedAt = DateTime.Now;

            var userToCreate = _mapper.Map<User>(user);
            _context.Users.Add(userToCreate);
            await _context.SaveChangesAsync();
            return Ok("User registered successfully");
        }

        //Logging in a user
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginUser.Email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(loginUser.UserPassword, user.UserPassword))
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new {Token = token });
        }
    }
}
