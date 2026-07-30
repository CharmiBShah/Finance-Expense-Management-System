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
