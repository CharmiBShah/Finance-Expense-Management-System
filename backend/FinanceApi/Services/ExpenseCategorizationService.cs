using FinanceApi.Models;

namespace FinanceApi.Services
{
    public static class ExpenseCategorizationService
    {
        public static string Categorize(string title, string notes)
        {
            var text = (title + " " + notes).ToLowerInvariant();

            if (text.Contains("coffee") || text.Contains("latte") || text.Contains("cafe")) return "Coffee";
            if (text.Contains("grocer") || text.Contains("supermarket") || text.Contains("market")) return "Groceries";
            if (text.Contains("uber") || text.Contains("taxi") || text.Contains("lyft")) return "Transport";
            if (text.Contains("rent") || text.Contains("mortgage")) return "Housing";
            if (text.Contains("electric") || text.Contains("water") || text.Contains("internet")) return "Utilities";
            if (text.Contains("restaurant") || text.Contains("dinner") || text.Contains("lunch")) return "Dining";
            if (text.Contains("gift") || text.Contains("donation") || text.Contains("charity")) return "Gifts";
            if (text.Contains("flight") || text.Contains("hotel") || text.Contains("travel")) return "Travel";

            return "Uncategorized";
        }
    }
}
