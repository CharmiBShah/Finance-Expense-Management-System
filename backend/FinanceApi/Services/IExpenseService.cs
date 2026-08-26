using FinanceApi.DTOs;

namespace FinanceApi.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseResponseDto>> GetExpensesAsync(int userId);

        Task<ExpenseResponseDto?> GetExpenseByIdAsync(int id, int userId);

        Task<ExpenseResponseDto> CreateExpenseAsync(ExpenseCreateDto expenseDto,int userId);

        Task<bool> UpdateExpenseAsync(int id,ExpenseUpdateDto expenseDto,int userId);

        Task<bool> DeleteExpenseAsync(int id, int userId);
    }
}