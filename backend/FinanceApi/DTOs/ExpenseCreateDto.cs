using System.ComponentModel.DataAnnotations;

namespace FinanceApi.DTOs
{
    public class ExpenseCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;


        [Required]
        [Range(0.01, 1000000)]
        public decimal Amount { get; set; }


        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;


        [Required]
        public DateTime Date { get; set; }


        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}