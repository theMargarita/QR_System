using Application.DTOs.TransactionFolder;
using Application.IServices;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly QrDbContext _context;
        private readonly ILogger _logger;
        public TransactionService(QrDbContext context, ILogger<TransactionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<bool> Closetransaction(int transactionId)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request)
        {
            var item = new Transaction
            {
                Quantity = request.Quantity,
                PriceEach = request.PriceEach,
                CreatedAt = request.CreatedAt,
                ProductId = request.ProductId,
                TabId = request.TabId,
                UserId = request.UserId
            };

            if(item == null)
            {
                _logger.LogError($"Failed to create transaction from request: {request}");
            }

             _context.Transactions.Add(item);
            await _context.SaveChangesAsync();

            return TransactionResponse.FromTransaction(item);
        }

        public Task<List<TransactionResponse>> GetTransactionsByContextPartIdAsync(Guid contextPartId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TransactionResponse>> GetTransactionsByUserTabIdAsync(int userTabId)
        {
            throw new NotImplementedException();
        }
    }
}
