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

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        }
    }
}
