using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;
using Application.Helpers;
using Application.IServices;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ContextPartService : IContextPartService
    {
        private readonly QrDbContext _context;
        private readonly ILogger _logger;

        public ContextPartService(ILogger<ContextPartService> logger, QrDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<bool> ActivateTableAsync(Guid contextPartId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ContextPartResponse?> CreateContextPartAsync(CreateContexPartRequest request)
        {
            var context = await _context.Contexts.FindAsync(request.ContextId);
            if (context == null)
            {
                _logger.LogWarning($"Context with ID {request.ContextId} not found.");
                return null;
            }

            var qrToken = await QrGenerateHelper.GenerateUniqueForContextPart(_context);

            var newPart = new ContextPart
            {
                Name = request.Name,
                QrToken = qrToken,
                IsActive = request.IsActive,
                Context = context
            };

            _context.ContextParts.Add(newPart);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Created new ContextPart with ID: {newPart.Id} and QR Token: {newPart.QrToken}");

            return ContextPartResponse.FromEntity(newPart);
        }

        public string GenerateUniqueQRToken()
        {
            string qrToken;
            do
            {
                //generate a random token
                qrToken = $"CTX_{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
            }
            while (_context.ContextParts.QrTokenExist(qrToken).Any()); //återstår att se om denna fungerar

            return qrToken;
        }

        public async Task<int> GetActiveUserCountAsync(Guid contextPartId)
        {
            var count = _context.ContextParts.Where(cp => cp.Id == contextPartId)
                .SelectMany(cp => cp.UserTabs)
                .Where(ut => !ut.IsClosed)
                .CountAsync();

            if (count == null)
            {
                _logger.LogError($"Failed to retrieve active user count for ContextPart ID {contextPartId}");
                return 0;
            }

            return await count;
        }

        public async Task<ContextPartResponse?> ScanQrTokenAsync(string qrToken)
        {
            _logger.LogInformation($"Scanning QR token: {qrToken}");

            var qr = _context.ContextParts.QrTokenExist(qrToken).FirstOrDefaultAsync();

            var contextPart = await _context.ContextParts
           .Include(cp => cp.Context)
               .ThenInclude(c => c.Owner)
           .Include(cp => cp.UserTabs.Where(ut => ut.Status == TabStatus.Open))
           .FirstOrDefaultAsync(cp => cp.QrToken == qrToken && cp.IsActive);

            _logger.LogInformation($"Scanning QR token: {qrToken}. ContextPart found: {contextPart != null}");

            if (contextPart == null)
            {
                _logger.LogWarning($"No active ContextPart found with token {qrToken}");
                return null;
            }

            _logger.LogInformation(
         $"Found ContextPart {contextPart.Name} in Context {contextPart.Context.Name} owned by {contextPart.Context.Owner.Name}");

            return ContextPartResponse.FromEntity(contextPart);
        }

        public async Task<int?> GetUserActiveTabAsync(Guid cpId)
        {
            var count = _context.UserTabs
                .Where(ut => ut.ContextPartId == cpId && ut.Status == TabStatus.Open)
                .CountAsync();

            if (count == null)
            {
                _logger.LogError($"Failed to retrieve active tab count for ContextPart ID {cpId}");
                return null;
            }

            return await count;
        }

        //tycker inte riktigt att denna är relevant
        public async Task<bool> CheckTable(int contextPartId)
        {
            //var table = await _context.ContextParts.FindAsync(contextPartId);

            var check = _context.ContextParts.CheckTables();

            if (check == null)
            {
                _logger.LogWarning($"ContextPart with ID {contextPartId} does not exist.");
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveContextPartAsync(Guid contextPartId)
        {
            //var id = await _context.ContextParts.FindAsync(contextPartId);
            var id = await _context.ContextParts
                .Include(cp => cp.UserTabs)
                .FirstOrDefaultAsync(cp => cp.Id == contextPartId);

            if (id == null)
            {
                _logger.LogWarning($"Attempted to remove non-existent ContextPart with ID {contextPartId}");
                return false;
            }

            _context.ContextParts.Remove(id);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfully removed ContextPart with ID: {contextPartId}");
            return true;
        }

        public async Task<ContextPartResponse?> UpdateContextPartAsync(Guid contextPartId, string newName)
        {
            var contextPart = await _context.ContextParts.FindAsync(contextPartId);

            if (contextPart == null)
            {
                _logger.LogWarning($"ContextPart with ID {contextPartId} not found.");
                return null;
            }

            contextPart.Name = newName;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Updated ContextPart {contextPartId} name to '{newName}'");

            return ContextPartResponse.FromEntity(contextPart);
        }

        public async Task<ContextPartResponse?> GetOrCreateActiveTabAsync(Guid cpId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ContextPartResponse> GetContextPartByIdAsync(Guid contextPartId)
        {
            var contextPart = await _context.ContextParts
               .Include(cp => cp.Context)
               .Include(cp => cp.UserTabs.Where(ut => ut.Status == TabStatus.Open))
               .FirstOrDefaultAsync(cp => cp.Id == contextPartId);

            if (contextPart == null)
            {
                _logger.LogWarning($"ContextPart with ID {contextPartId} not found.");
                return null;
            }

            return ContextPartResponse.FromEntity(contextPart);
        }
        public async Task<IEnumerable<ContextPartResponse>> GetAllContextPartsAsync()
        {
            var cps = await _context.ContextParts
            .GetAll().ToListAsync();

            //return contextParts.Select(cp => ContextPartResponse.FromEntity(cp));

            return cps.Select(cp => new ContextPartResponse
            {
                Id = cp.Id,
                ContextId = cp.Context.Id,
                Name = cp.Name,
                ContextName = cp.Context.Name,
                QrToken = cp.QrToken,
                IsActive = cp.IsActive,
                ActiveUserCount = cp.UserTabs.ToList().Count(),
            }).ToList();
        }

        public async Task<IEnumerable<ContextPartResponse>> GetAllContextPartWithContxtId(Guid id)
        {
            var cps = await _context.ContextParts.GetAllWithContextId(id).ToListAsync();


            //return cps.Select(cp => ContextPartResponse.FromEntity(cp));

            return cps.Select(cp => new ContextPartResponse
            {
                Id = id,
                ContextId = cp.Context.Id,
                Name = cp.Name,
                ContextName = cp.Context.Name,
                QrToken = cp.QrToken,
                IsActive = cp.IsActive,
            }).ToList();
        }
    }
}
