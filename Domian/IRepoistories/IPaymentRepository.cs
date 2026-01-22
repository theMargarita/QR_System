using Domain.Models;

namespace Domain.IRepositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment?> GetPaymentWithDetailsAsync(int id); // Inkluderar Tab, User

        // get payments by relations
        Task<IEnumerable<Payment>> GetPaymentsByTabIdAsync(int tabId);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId);

        // calcualtions and statistics
        Task<decimal> GetTotalPaidByTabIdAsync(int tabId);
        Task<decimal> GetTotalPaidByUserIdAsync(int userId, DateTime? from = null, DateTime? to = null);

        // Rapporter
        Task<IEnumerable<Payment>> GetRecentPaymentsAsync(int count = 10);
    }
}
