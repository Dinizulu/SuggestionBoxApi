using System;
using System.Collections.Generic;

namespace SuggestionBoxApi.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Suggestion> Suggestions { get; set; } = new List<Suggestion>();
}
