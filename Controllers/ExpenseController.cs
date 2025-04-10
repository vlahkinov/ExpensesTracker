using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Services;
using ExpenseTracker.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly ICategoryService _categoryService;

        public ExpenseController(IExpenseService expenseService, ICategoryService categoryService)
        {
            _expenseService = expenseService;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Gets all expenses
        /// </summary>
        /// <returns>A list of expenses</returns>
        /// <response code="200">Returns the list of expenses</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllExpenses()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();
            return Ok(expenses);
        }

        /// <summary>
        /// Gets a specific expense by id
        /// </summary>
        /// <param name="id">The expense id</param>
        /// <returns>The expense</returns>
        /// <response code="200">Returns the expense</response>
        /// <response code="404">If the expense doesn't exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        /// <summary>
        /// Gets all expenses for a specific category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns>A list of expenses for the specified category</returns>
        /// <response code="200">Returns the list of expenses</response>
        /// <response code="404">If the category doesn't exist</response>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesByCategory(int categoryId)
        {
            if (!await _categoryService.CategoryExistsAsync(categoryId))
            {
                return NotFound();
            }

            var expenses = await _expenseService.GetExpensesByCategoryAsync(categoryId);
            return Ok(expenses);
        }

        /// <summary>
        /// Creates a new expense
        /// </summary>
        /// <param name="expenseDto">The expense data</param>
        /// <returns>The created expense</returns>
        /// <response code="201">Returns the newly created expense</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="404">If the associated category doesn't exist</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExpenseDto>> CreateExpense(ExpenseCreateDto expenseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _categoryService.CategoryExistsAsync(expenseDto.CategoryId))
            {
                return NotFound($"Category with ID {expenseDto.CategoryId} not found.");
            }

            var createdExpense = await _expenseService.AddExpenseAsync(expenseDto);
            return CreatedAtAction(nameof(GetExpense), new { id = createdExpense.Id }, createdExpense);
        }

        /// <summary>
        /// Updates an expense
        /// </summary>
        /// <param name="id">The expense id</param>
        /// <param name="expenseUpdateDto">The updated expense data</param>
        /// <returns>No content</returns>
        /// <response code="204">If the expense was updated</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="404">If the expense or associated category doesn't exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateExpense(int id, ExpenseUpdateDto expenseUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the expense exists
            if (!await _expenseService.ExpenseExistsAsync(id))
            {
                return NotFound();
            }

            // Check if the associated category exists
            if (!await _categoryService.CategoryExistsAsync(expenseUpdateDto.CategoryId))
            {
                return NotFound($"Category with ID {expenseUpdateDto.CategoryId} not found.");
            }

            // Update the expense
            await _expenseService.UpdateExpenseAsync(id, expenseUpdateDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an expense
        /// </summary>
        /// <param name="id">The expense id</param>
        /// <returns>No content</returns>
        /// <response code="204">If the expense was deleted</response>
        /// <response code="404">If the expense doesn't exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if (!await _expenseService.ExpenseExistsAsync(id))
            {
                return NotFound();
            }

            await _expenseService.DeleteExpenseAsync(id);
            return NoContent();
        }
    }
} 