using ExpenseTracker.Models;

namespace ExpenseTracker.Data
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense?> GetExpenseByIdAsync(int id);
        Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(int categoryId);
        Task AddExpenseAsync(Expense expense);
        Task UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(int id);
        Task<bool> ExpenseExistsAsync(int id);
    }
} 