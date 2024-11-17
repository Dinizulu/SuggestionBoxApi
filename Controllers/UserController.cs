using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Repositories;

namespace SuggestionBoxApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Getting all users
        [HttpGet("Getting_all_users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUAsync();
            return Ok(users);
        }

        //Geting a user by their unique id
        [HttpGet("Get_by_id")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null)
            {
                return NotFound($"User with id: {id} is not registered");
            }
            return Ok(user);
        }

        //Adding a user
        [HttpPost("Adding_a_user")]
        public async Task<ActionResult<UserDto>> AddingYserAsync(UserDto user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Empty fields are not allowed");
            }
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new {id = user.UserId}, user);
        }

        //Deleting a user
        [HttpDelete("Delete_a_user")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            await _userRepository.SaveChangesAsync();
            return Ok($"User with id: {id} has been deleted");
        }
    }
}
