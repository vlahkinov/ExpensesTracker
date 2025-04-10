using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
} 