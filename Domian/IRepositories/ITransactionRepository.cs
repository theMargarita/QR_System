using System.Transactions;

namespace Domain.IRepositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        // Hämta transactions med relaterad data
        Task<Transaction?> GetTransactionWithDetailsAsync(int id); // Inkluderar Product, Tab, User

        // Hämta transactions baserat på relationer
        Task<IEnumerable<Transaction>> GetTransactionsByTabIdAsync(int tabId);
        Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int userId);
        Task<IEnumerable<Transaction>> GetTransactionsByProductIdAsync(int productId);

        // Statistik och beräkningar
        Task<decimal> GetTotalByTabIdAsync(int tabId);
        Task<decimal> GetTotalByUserIdAsync(int userId, DateTime? from = null, DateTime? to = null);
        Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int count = 10);

        // Produktstatistik (för rapporter)
        Task<Dictionary<int, int>> GetProductSalesCountAsync(DateTime? from = null, DateTime? to = null);
    }
}
