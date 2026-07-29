using FinanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Data
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses => Set<Expense>();
    }
}
