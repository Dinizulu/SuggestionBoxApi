using SuggestionBoxApi.Models;

namespace SuggestionBoxApi.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
