namespace SuggestionBoxApi.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string? Email { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string UserPassword { get; set; } = null!;
    }
}
