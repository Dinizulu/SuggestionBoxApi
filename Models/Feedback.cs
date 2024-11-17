using System;
using System.Collections.Generic;

namespace SuggestionBoxApi.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? SuggestionId { get; set; }

    public string FeedbackText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Suggestion? Suggestion { get; set; }
}
