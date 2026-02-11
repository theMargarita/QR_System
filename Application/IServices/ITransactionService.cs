using Application.DTOs.TransactionFolder;

namespace Application.IServices
{
    public interface ITransactionService
    {
        Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request);
        Task<bool>Closetransaction(int transactionId);
        Task<List<TransactionResponse>> GetTransactionsByUserTabIdAsync(int userTabId);
        Task<List<TransactionResponse>> GetTransactionsByContextPartIdAsync(Guid contextPartId);
    }
}
