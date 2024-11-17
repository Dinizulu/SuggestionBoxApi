using System;
using System.Collections.Generic;

namespace SuggestionBoxApi.Models;

public partial class Suggestion
{
    public int SuggestionId { get; set; }

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    public string SuggestionText { get; set; } = null!;

    public DateTime? SubmittedAt { get; set; }

    public bool IsAnonymous { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User? User { get; set; }
}
