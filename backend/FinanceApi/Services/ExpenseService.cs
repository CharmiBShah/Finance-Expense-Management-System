using FinanceApi.Data;
using FinanceApi.DTOs;
using FinanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly FinanceDbContext _context;

        public ExpenseService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExpenseResponseDto>> GetExpensesAsync()
        {
            var expenses = await _context.Expenses
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            return expenses.Select(e => new ExpenseResponseDto
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                Category = e.Category,
                Date = e.Date,
                Notes = e.Notes
            });
        }

        public async Task<ExpenseResponseDto?> GetExpenseByIdAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return null;

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

        public async Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseCreateDto expenseDto)
        {
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
    }
}