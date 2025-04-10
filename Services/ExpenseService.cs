using AutoMapper;
using ExpenseTracker.Data;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync()
        {
            var expenses = await _repository.GetAllExpensesAsync();
            return _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
        }

        public async Task<ExpenseDto?> GetExpenseByIdAsync(int id)
        {
            var expense = await _repository.GetExpenseByIdAsync(id);
            return expense == null ? null : _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<ExpenseDto> AddExpenseAsync(ExpenseCreateDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            await _repository.AddExpenseAsync(expense);
            
            // Reload the expense with category information
            var savedExpense = await _repository.GetExpenseByIdAsync(expense.Id);
            return _mapper.Map<ExpenseDto>(savedExpense);
        }

        public async Task UpdateExpenseAsync(ExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            await _repository.UpdateExpenseAsync(expense);
        }

        public async Task UpdateExpenseAsync(int id, ExpenseUpdateDto expenseUpdateDto)
        {
            // Get existing expense or create a new one if not found
            var expense = await _repository.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                expense = new Expense { Id = id };
            }

            // Update properties from DTO
            expense.Title = expenseUpdateDto.Title;
            expense.Description = expenseUpdateDto.Description;
            expense.Amount = expenseUpdateDto.Amount;
            expense.Date = expenseUpdateDto.Date;
            expense.CategoryId = expenseUpdateDto.CategoryId;

            // Save changes
            await _repository.UpdateExpenseAsync(expense);
        }

        public async Task DeleteExpenseAsync(int id)
        {
            await _repository.DeleteExpenseAsync(id);
        }

        public async Task<bool> ExpenseExistsAsync(int id)
        {
            return await _repository.ExpenseExistsAsync(id);
        }

        public async Task<IEnumerable<ExpenseDto>> GetExpensesByCategoryAsync(int categoryId)
        {
            var expenses = await _repository.GetExpensesByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
        }
    }
} 