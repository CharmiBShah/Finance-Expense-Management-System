using FinanceApi.Common;
using Microsoft.AspNetCore.Mvc;
using FinanceApi.Services;
using FinanceApi.DTOs;

namespace FinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        /// <summary>
        /// Retrieves all expenses sorted by date in descending order.
        /// </summary>
        /// <returns>A list of expenses.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ExpenseResponseDto>>>> GetExpenses()
        {
            var expenses = await _expenseService.GetExpensesAsync();

            var response = new ApiResponse<IEnumerable<ExpenseResponseDto>>(
                true,
                "Expenses retrieved successfully",
                expenses
            );

            return Ok(response);
        }


        /// <summary>
        /// Retrieves a specific expense by its ID.
        /// </summary>
        /// <param name="id">The ID of the expense.</param>
        /// <returns>The requested expense if found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ExpenseResponseDto>>> GetExpense(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);

            if (expense == null)
            {
                var errorResponse = new ApiResponse<ExpenseResponseDto>(
                    false,
                    "Expense not found"
                );

                return NotFound(errorResponse);
            }

            var response = new ApiResponse<ExpenseResponseDto>(
                true,
                "Expense retrieved successfully",
                expense
            );

            return Ok(response);
        }


        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="expenseDto">The expense details.</param>
        /// <returns>The newly created expense.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ExpenseResponseDto>>> CreateExpense(
    ExpenseCreateDto expenseDto)
        {
            var createdExpense = await _expenseService.CreateExpenseAsync(expenseDto);

            var response = new ApiResponse<ExpenseResponseDto>(
                true,
                "Expense created successfully",
                createdExpense
            );

            return CreatedAtAction(
                nameof(GetExpense),
                new { id = createdExpense.Id },
                response);
        }


        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        /// <param name="id">The ID of the expense.</param>
        /// <param name="expenseDto">The updated expense details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateExpense(
    int id,
    ExpenseUpdateDto expenseDto)
        {
            var updated = await _expenseService.UpdateExpenseAsync(id, expenseDto);

            if (!updated)
            {
                var errorResponse = new ApiResponse<object>(
                    false,
                    "Expense not found"
                );

                return NotFound(errorResponse);
            }

            var response = new ApiResponse<object>(
                true,
                "Expense updated successfully"
            );

            return Ok(response);
        }


        /// <summary>
        /// Deletes an expense.
        /// </summary>
        /// <param name="id">The ID of the expense.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteExpense(int id)
        {
            var deleted = await _expenseService.DeleteExpenseAsync(id);

            if (!deleted)
            {
                var errorResponse = new ApiResponse<object>(
                    false,
                    "Expense not found"
                );

                return NotFound(errorResponse);
            }

            var response = new ApiResponse<object>(
                true,
                "Expense deleted successfully"
            );

            return Ok(response);
        }
    }
}
