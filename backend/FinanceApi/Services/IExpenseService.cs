using FinanceApi.DTOs;

namespace FinanceApi.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseResponseDto>> GetExpensesAsync();

        Task<ExpenseResponseDto?> GetExpenseByIdAsync(int id);

        Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseCreateDto expenseDto);

        Task<bool> UpdateExpenseAsync(int id, ExpenseUpdateDto expenseDto);

        Task<bool> DeleteExpenseAsync(int id);
    }
}