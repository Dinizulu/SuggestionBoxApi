using System;
using System.Collections.Generic;

namespace SuggestionBoxApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string UserPassword { get; set; } = null!;

    public string Role { get; set; } = string.Empty;

    public virtual ICollection<Suggestion> Suggestions { get; set; } = new List<Suggestion>();
}
