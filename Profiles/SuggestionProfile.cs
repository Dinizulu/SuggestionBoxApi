using AutoMapper;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Models;

namespace SuggestionBoxApi.Profiles
{
    public class SuggestionProfile : Profile
    {
        public SuggestionProfile()
        {
            CreateMap<Suggestion,SuggestionDto>().ReverseMap();
        }
    }
}
