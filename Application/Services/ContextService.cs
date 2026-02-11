using Application.DTOs.ContextFolder.Request;
using Application.DTOs.ContextFolder.Response;
using Application.Helpers;
using Application.IServices;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ContextService : IContextService
    {
        private readonly QrDbContext _context;
        private readonly ILogger<UserService> _logger;
        private readonly QrCodeService _qrCodeService;

        public ContextService(ILogger<UserService> logger, QrDbContext context, QrCodeService qrCodeService)
        {
            _logger = logger;
            _context = context;
            _qrCodeService = qrCodeService;
        }

        public async Task<ContextResponse?> CreateContextAsync(QrContextPartRequest newContext)
        {
            //var uniqueToken = GenerateUniqueQRToken();

            var addItem = new Context
            {
                Name = newContext.Name,
                QrToken = newContext.QrToken,
                OwnerId = newContext.OwnerId,
                ContextPartIsUnique = newContext.ContextPartIsUnique //??? - this is a bool - might be to much
            };

            _context.Contexts.Add(addItem);
            await _context.SaveChangesAsync();

            if (addItem == null)
            {
                _logger.LogError("Failed to create context entity from request.");
                if (addItem.QrToken == null)
                {
                    _logger.LogWarning("QrToken is null in the create context request.");
                    return null;
                }
                return null;
            }
            //newContext.QrToken = uniqueToken;

            return ContextResponse.FromContext(addItem);
        }

        public async Task<IEnumerable<ContextResponse>> GetAllContextsAsync()
        {

            return _context.Contexts.Select(c => ContextResponse.FromContext(c));
        }

        public async Task<ContextResponse?> GetContextByIdAsync(Guid id)
        {
            var contextId = await _context.Contexts.FindAsync(id);

            if (contextId == null)
            {
                _logger.LogWarning($"Context with ID {id} not found.");
                return null;
            }

            return ContextResponse.FromContext(contextId);
        }

        // i think this one should be for events maybe? 
        public async Task<IEnumerable<ContextResponse>> SearchContext(string searchTerm)
        {
            StringHelper.Normalize(searchTerm);

            if (searchTerm == null || string.IsNullOrWhiteSpace(searchTerm))
            {
                _logger.LogWarning($"Search term is invalid: {searchTerm}");
                return null;
            }

            return await _context.Contexts
                 .Search(searchTerm)
                 .Select(c => ContextResponse
                 .FromContext(c))
                 .ToListAsync();
        }

        public async Task<bool> RemoveContextAsync(Guid id)
        {
            var findId = await _context.Contexts.FindAsync(id);

            if (findId == null)
            {
                _logger.LogWarning($"Context with ID {id} not found for deletion.");
                return false;
            }

            _context.Contexts.Remove(findId);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Context with ID {findId} has been deleted.");
            return true;
        }
        // Generates a unique QR token for a context when a new context is created 
        //public string GenerateUniqueQRToken()
        //{
        //    string qrToken;
        //    do
        //    {
        //        //generate a random token
        //        qrToken = $"CTX_{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
        //    }
        //    while (_context.Contexts.QRTokenExistsAsync(qrToken).Result);
        //    return qrToken;
        //}
    }
}
//public Task<bool> ActivateContextAsync(int contextId)
//{
//    throw new NotImplementedException();
//}

//public Task<bool> ContextExistsAsync(int contextId)
//{
//    throw new NotImplementedException();
//}