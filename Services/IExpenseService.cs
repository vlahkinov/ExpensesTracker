using ExpenseTracker.DTOs;

namespace ExpenseTracker.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync();
        Task<ExpenseDto?> GetExpenseByIdAsync(int id);
        Task<ExpenseDto> AddExpenseAsync(ExpenseCreateDto expenseDto);
        Task UpdateExpenseAsync(ExpenseDto expenseDto);
        Task UpdateExpenseAsync(int id, ExpenseUpdateDto expenseUpdateDto);
        Task DeleteExpenseAsync(int id);
        Task<bool> ExpenseExistsAsync(int id);
        Task<IEnumerable<ExpenseDto>> GetExpensesByCategoryAsync(int categoryId);
    }
} 