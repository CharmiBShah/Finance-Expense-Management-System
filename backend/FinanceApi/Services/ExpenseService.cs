using FinanceApi.Data;
using FinanceApi.DTOs;
using FinanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly FinanceDbContext _context;

        private readonly ILogger<ExpenseService> _logger;

        public ExpenseService(
            FinanceDbContext context,
            ILogger<ExpenseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ExpenseResponseDto>> GetExpensesAsync()
        {
            var expenses = await _context.Expenses
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            return expenses.Select(MapToResponseDto);
        }

        public async Task<ExpenseResponseDto?> GetExpenseByIdAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return null;

            return MapToResponseDto(expense);
        }

        public async Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseCreateDto expenseDto)
        {

            _logger.LogInformation("Creating expense with title: {Title}", expenseDto.Title);

            var expense = new Expense
            {
                Title = expenseDto.Title,
                Amount = expenseDto.Amount,
                Category = expenseDto.Category,
                Date = expenseDto.Date,
                Notes = expenseDto.Notes ?? string.Empty
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Expense created successfully with Id: {Id}", expense.Id);

            return MapToResponseDto(expense);
        }

        public async Task<bool> UpdateExpenseAsync(int id, ExpenseUpdateDto expenseDto)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return false;

            expense.Title = expenseDto.Title;
            expense.Amount = expenseDto.Amount;
            expense.Category = expenseDto.Category;
            expense.Date = expenseDto.Date;
            expense.Notes = expenseDto.Notes ?? string.Empty;

            //_context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return false;

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return true;
        }

        private ExpenseResponseDto MapToResponseDto(Expense expense)
        {
            return new ExpenseResponseDto
            {
                Id = expense.Id,
                Title = expense.Title,
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date,
                Notes = expense.Notes
            };
        }
    }
}