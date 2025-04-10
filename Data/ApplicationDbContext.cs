using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some initial categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Housing", Description = "Rent, mortgage, repairs, etc." },
                new Category { Id = 2, Name = "Transportation", Description = "Public transport, car expenses, etc." },
                new Category { Id = 3, Name = "Food", Description = "Groceries, restaurants, etc." },
                new Category { Id = 4, Name = "Utilities", Description = "Electricity, water, internet, etc." },
                new Category { Id = 5, Name = "Entertainment", Description = "Movies, games, etc." }
            );
        }
    }
} 