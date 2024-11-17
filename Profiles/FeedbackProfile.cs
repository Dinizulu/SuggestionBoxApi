using AutoMapper;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Models;

namespace SuggestionBoxApi.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback,FeedbackDto>().ReverseMap();
        }
    }
}
