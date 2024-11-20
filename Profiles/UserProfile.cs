using AutoMapper;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Models;

namespace SuggestionBoxApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }

    public class CreateUserProfile : Profile
    {
        public CreateUserProfile()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}
