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
        public async Task<ActionResult<IEnumerable<ExpenseResponseDto>>> GetExpenses()
        {
            var expenses = await _expenseService.GetExpensesAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseResponseDto>> GetExpense(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);

            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseResponseDto>> CreateExpense(ExpenseCreateDto expenseDto)
        {
            var createdExpense = await _expenseService.CreateExpenseAsync(expenseDto);

            return CreatedAtAction(
                nameof(GetExpense),
                new { id = createdExpense.Id },
                createdExpense);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, ExpenseUpdateDto expenseDto)
        {
            var updated = await _expenseService.UpdateExpenseAsync(id, expenseDto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var deleted = await _expenseService.DeleteExpenseAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
