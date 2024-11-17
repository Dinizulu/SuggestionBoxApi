namespace SuggestionBoxApi.DTOs
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }

        public int? SuggestionId { get; set; }

        public string FeedbackText { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
    }
}
