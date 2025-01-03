﻿namespace SuggestionBoxApi.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;
        public ICollection<SuggestionDto> Suggestions { get; set; }
    }
}
