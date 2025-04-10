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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>A list of categories</returns>
        /// <response code="200">Returns the list of categories</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Gets a specific category by id
        /// </summary>
        /// <param name="id">The category id</param>
        /// <returns>The category</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="404">If the category doesn't exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="categoryDto">The category data</param>
        /// <returns>The created category</returns>
        /// <response code="201">Returns the newly created category</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCategory = await _categoryService.AddCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        /// <summary>
        /// Updates a category
        /// </summary>
        /// <param name="id">The category id</param>
        /// <param name="categoryUpdateDto">The updated category data</param>
        /// <returns>No content</returns>
        /// <response code="204">If the category was updated</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="404">If the category doesn't exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the category exists
            if (!await _categoryService.CategoryExistsAsync(id))
            {
                return NotFound();
            }

            // Update the category
            await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a category
        /// </summary>
        /// <param name="id">The category id</param>
        /// <returns>No content</returns>
        /// <response code="204">If the category was deleted</response>
        /// <response code="404">If the category doesn't exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!await _categoryService.CategoryExistsAsync(id))
            {
                return NotFound();
            }

            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
} 