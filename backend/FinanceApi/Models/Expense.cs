using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using FinanceApi.Data;
using FinanceApi.DTOs;
using Microsoft.EntityFrameworkCore;
namespace FinanceApi.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; } = string.Empty;
    }
}
