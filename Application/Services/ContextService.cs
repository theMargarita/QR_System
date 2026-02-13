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
        //private readonly IQrCodeService _qrCodeService;
        //private readonly IContextPartService _cpService;

        public ContextService(ILogger<UserService> logger, QrDbContext context, IQrCodeService qrCodeService, IContextPartService cpService)
        {
            _logger = logger;
            _context = context;
            _qrCodeService = qrCodeService;
            _cpService = cpService;
        }

        public async Task<ContextResponse?> CreateContextAsync(CreateContextRequest request)
        {
            //var uniqueToken = GenerateUniqueQRToken();
            var owner = await _context.Owners.FindAsync(request.OwnerId);
            if (owner == null)
                _logger.LogInformation($"Could not find owner with id: {owner}");

            var qrToken = await QrGenerateHelper.GenerateUniqueForContext(_context);

            var context = new Context
            {
                Name = request.Name,
                OwnerId = request.OwnerId,
                Owner = owner,
                QrToken = qrToken,
                IsActive = true,
                ContextPartIsUnique = request.ContextPartIsUnique,
            };

            await _context.Contexts.AddAsync(context);
            await _context.SaveChangesAsync();

            //newContext.QrToken = uniqueToken;

            return ContextResponse.FromContext(context);
        }

        public async Task<CreatedEventResponse?> CreateEventAsync(CreateContextRequest request)
        {
            //var uniqueToken = GenerateUniqueQRToken();
            var owner = await _context.Owners.FindAsync(request.OwnerId);
            if (owner == null)
                _logger.LogInformation($"Could not find owner with id: {owner}");

            var qrToken = await QrGenerateHelper.GenerateUniqueForContext(_context);

            var context = new Context
            {
                Name = request.Name,
                OwnerId = request.OwnerId,
                Owner = owner,
                QrToken = qrToken,
                IsActive = true,
                ContextPartIsUnique = request.ContextPartIsUnique,

                IsTemporary = request.IsTemporary == true,
                StartsAt = request.StartsAt,
                ExpiresAt = request.ExpiresAt
            };

            await _context.Contexts.AddAsync(context);
            await _context.SaveChangesAsync();

            //newContext.QrToken = uniqueToken;

            _logger.LogInformation($"Created Context {context.Id} for Owner {owner.Name}");

            return CreatedEventResponse.FromContext(context);
        }

        public async Task<IEnumerable<ContextResponse>> GetAllContextsAsync()
        {

            return _context.Contexts.Select(c => ContextResponse.FromContext(c));
        }

        public async Task<ContextResponse?> GetContextByIdAsync(Guid id)
        {
            var context = await _context.Contexts
          .Include(c => c.Owner)
          .Include(c => c.Parts)
          .FirstOrDefaultAsync(c => c.Id == id);

            if (context == null)
            {
                _logger.LogWarning($"Context with ID {id} not found.");
                return null;
            }

            return ContextResponse.FromContext(context);
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

            //return await _context.Contexts
            //     .Search(searchTerm)
            //     .Select(c => ContextResponse
            //     .FromContext(c))
            //     .ToListAsync();

            var normalizedTerm = searchTerm.Trim().ToLower();

            return await _context.Contexts
                .Include(c => c.Owner)
                .Where(c => c.Name.ToLower().Contains(normalizedTerm) ||
                           c.Owner.Name.ToLower().Contains(normalizedTerm))
                .Select(c => ContextResponse.FromContext(c))
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
        public string GenerateUniqueQRToken()
        {
            string qrToken;
            do
            {
                //generate a random token
                qrToken = $"CTX_{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
            }
            while (_context.Contexts.QRTokenExistsAsync(qrToken).Result);
            return qrToken;
        }
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