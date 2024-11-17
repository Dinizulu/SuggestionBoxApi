namespace SuggestionBoxApi.DTOs
{
    public class SuggestionDto
    {
        public int SuggestionId { get; set; }

        public int? UserId { get; set; }

        public int? CategoryId { get; set; }

        public string SuggestionText { get; set; } = null!;

        public DateTime? SubmittedAt { get; set; }

        public bool IsAnonymous { get; set; }
    }
}
