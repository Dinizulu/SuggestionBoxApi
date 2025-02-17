using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
        private readonly AuthService _authenticationService;
        private readonly SuggboxContext _context;
        private readonly IMapper _mapper;

        public AuthenticationController(AuthService authenticationService, SuggboxContext context, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _context = context;
            _mapper = mapper;
        }

        //Login endpoint
        [HttpPost("Login")]
        public async Task<IActionResult> Login(CreateUserDto ligingUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == ligingUser.Email && x.UserPassword == ligingUser.UserPassword);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _authenticationService.GenerateToken(user);
            return Ok(new {Token =  token});
        }

        //Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto newUser)
        {
            if(await _context.Users.AnyAsync(x => x.Email == newUser.Email))
            {
                return Unauthorized("Email entered already registered");
            }

            newUser.CreatedAt = DateTime.Now;
            newUser.Role = "User";

            var userToRegister = _mapper.Map<User>(newUser);
            _context.Users.Add(userToRegister);
            await _context.SaveChangesAsync();
            return Ok("Registered");
        }
    }
}
