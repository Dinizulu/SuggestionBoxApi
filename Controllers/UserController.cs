using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Models;
using SuggestionBoxApi.Repositories;

namespace SuggestionBoxApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private SuggestionBoxRepository<User> _boxRepository;
        private readonly IMapper _mapper;

        public UserController(SuggestionBoxRepository<User> boxRepository, IMapper mapper)
        {
            _boxRepository = boxRepository;
            _mapper = mapper;
        }

        //Getting all users
        [HttpGet("Getting_all_users")]
        public async Task<ActionResult> GetAllUsersAsync()
        {
            var users =  await _boxRepository.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        //Geting a user by their unique id
        [HttpGet("Get_by_id")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _boxRepository.GetByIdAsync(id);
            if(user == null)
            {
                return NotFound($"User with id: {id} is not registered/contained");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        //Adding a user
        [HttpPost("Adding_a_user")]
        public async Task<ActionResult<UserDto>> AddingUserAsync(UserDto user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Empty fields are not allowed");
            }
            var userDto = _mapper.Map<User>(user);
            await _boxRepository.AddAsync(userDto);
            return CreatedAtAction(nameof(AddingUserAsync), new {id = user.UserId}, userDto);
        }

        //
        [HttpPut("Updating_a_user")]
        public async Task<IActionResult> UpdateUserAsync(int id, UserDto user)
        {
            if(id != user.UserId)
            {
                return BadRequest();
            }
            var userToUpdate = _mapper.Map<User>(user);
            await _boxRepository.UpdateAsync(userToUpdate);
            return Ok(userToUpdate);
        }

        //Deleting a user
        [HttpDelete("Delete_a_user")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            await _boxRepository.DeleteAsync(id);
            return Ok($"User with id: {id} has been deleted");
        }
    }
}
