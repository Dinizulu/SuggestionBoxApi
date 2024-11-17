using AutoMapper;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Models;

namespace SuggestionBoxApi.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category,CategoryDto>().ReverseMap();
        }
    }
}
