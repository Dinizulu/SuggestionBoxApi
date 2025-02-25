using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuggestionBoxApi.Models;

public partial class User
{
    public int UserId { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string UserPassword { get; set; } = null!;
    [Column(TypeName = "nvarchar(50)")]
    public string Role { get; set; } = string.Empty;

    public virtual ICollection<Suggestion> Suggestions { get; set; } = new List<Suggestion>();
}
