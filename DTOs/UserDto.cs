using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "nvarchar(255)")]
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string UserPassword { get; set; } = null!;
    }

    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string UserPassword { get; set; } = null!;
    }
}
