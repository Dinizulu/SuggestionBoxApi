namespace SuggestionBoxApi.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string? Email { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string UserPassword { get; set; } = null!;

        //Navigation property
        public ICollection<SuggestionDto> Suggestions { get; set; }
    }

    public class CreateUserDto
    {
        public int UserId { get; set; }

        public string? Email { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string UserPassword { get; set; } = null!;
    }
}
