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
    public class FeedbackController : ControllerBase
    {
        private readonly SuggestionBoxRepository<Feedback> _feedBoxRepository;
        private readonly SuggestionBoxRepository<Suggestion> _suggestionRepository;
        private readonly IMapper _mapper;

        public FeedbackController(SuggestionBoxRepository<Feedback> feedBoxRepository, SuggestionBoxRepository<Suggestion> suggestionRepository, IMapper mapper)
        {
            _feedBoxRepository = feedBoxRepository;
            _suggestionRepository = suggestionRepository;
            _mapper = mapper;
        }

        //Getting all the feedbacks
        [HttpGet("Get_feedbacks")]
        public async Task<ActionResult> GetFeedbacksAsync()
        {
            var feedbacks = await _feedBoxRepository.GetAllAsync();
            var feedbackDto = _mapper.Map<IEnumerable<Feedback>>(feedbacks);
            return Ok(feedbackDto);
        }

        //Getting a feedback based on unique id
        [HttpGet("Getting_a_feedback")]
        public async Task<ActionResult<FeedbackDto>> GetFeedbackAsync(int id)
        {
            var feedback = await _feedBoxRepository.GetByIdAsync(id);
            if (feedback == null)
            {
                return BadRequest($"Feedback of id: {id} is not contained");
            }

            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);
            return Ok(feedbackDto);
        }

        //Addint a feedbank
        [HttpPost("Adding_a_feedback")]
        public async Task<ActionResult<FeedbackDto>> AddingFeedbackAsync(FeedbackDto feed)
        {
            var feedback = await _feedBoxRepository.GetByIdAsync((int)feed.SuggestionId);
            if (feedback == null && !ModelState.IsValid)
            {
                return BadRequest("Empty fields not allowed");
            }

            var suggDto = _mapper.Map<Feedback>(feed);
            await _feedBoxRepository.AddAsync(suggDto);
            return CreatedAtAction(nameof(GetFeedbackAsync), new { id = feed.SuggestionId }, feed);
        }

        //Editing an existing feedback
        [HttpPut]
        public async Task<IActionResult> UpdateFeddAsync(int id, FeedbackDto feedback)
        {
            if (id != feedback.SuggestionId)
            {
                return BadRequest("Empty field not allowed");
            }

            var feedToUpdate = _mapper.Map<Feedback>(feedback);
            await _feedBoxRepository.UpdateAsync(feedToUpdate);
            return Ok(feedToUpdate);
        }

        ////Deleting an existing feedback
        //public async Task<ActionResult> DeleteFeedAsync(int id)
        //{
        //    await _feedBoxRepository.DeleteAsync(id);
        //    return Ok($"Feedback with id: {id} has been removed");
        //}
    }
}
