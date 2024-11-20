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
    public class SuggestionController : ControllerBase
    {
        private readonly SuggestionBoxRepository<Suggestion> _boxRepository;
        private readonly SuggestionBoxRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public SuggestionController(SuggestionBoxRepository<Suggestion> boxRepository, SuggestionBoxRepository<User> userRepository, IMapper mapper)
        {
            _boxRepository = boxRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //Getting all available suggestion per user
        [HttpGet("Getting_suggestions")]
        public async Task<ActionResult> GetAllSuggestionAsync()
        {
            var suggestions = await _boxRepository.GetAllAsync();
            var suggestionDtos = _mapper.Map<IEnumerable<CreateSuggestionDto>>(suggestions);
            return Ok(suggestionDtos);

        }

        //Getting suggestions by unique id
        [HttpGet("Getting_a_suggestion")]
        public async Task<ActionResult<SuggestionDto>> GetSuggestion(int id)
        {
            var suggestion = await _boxRepository.GetByIdAsync(id);
            if (suggestion == null)
            {
                return BadRequest($"Suggestion of id: {id} is not registered/contained");
            }
            var suggestionDto = _mapper.Map<SuggestionDto>(suggestion);
            return Ok(suggestionDto);
        }

        //Adding a suggestion
        [HttpPost("Adding_a_suggestion")]
        public async Task<ActionResult<CreateSuggestionDto>> AddSuggestionAsync(CreateSuggestionDto createSuggestionDto)
        {
            var user = await _boxRepository.GetByIdAsync((int)createSuggestionDto.UserId);
            if (!ModelState.IsValid && user == null)
            {
                return BadRequest("Empty fields are not required");
            }

            var suggestion = _mapper.Map<Suggestion>(createSuggestionDto);
            await _boxRepository.AddAsync(suggestion);
            return CreatedAtAction(nameof(GetSuggestion), new { id = createSuggestionDto.SuggestionId }, createSuggestionDto);
        }

        //Editing existing suggestion
        [HttpPut("Editing_a_suggestion")]
        public async Task<IActionResult> UpdateSuggestionAsync(int id, CreateSuggestionDto createSuggestionDto)
        {
            if (id != createSuggestionDto.SuggestionId)
            {
                return BadRequest("Empty fields not allowed");
            }

            var suggToUpdate = _mapper.Map<Suggestion>(createSuggestionDto);
            await _boxRepository.UpdateAsync(suggToUpdate);
            return Ok(suggToUpdate);
        }

        //Deleting a suggestion
        [HttpDelete("Delete_a_suggestion")]
        public async Task<ActionResult> DeleteSuggestionAsync(int id)
        {
            await _boxRepository.DeleteAsync(id);
            return Ok($"Suggestion with id: {id} has been removed");
        }
    }
}
