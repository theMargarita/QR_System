using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;
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
        private readonly IQrCodeService _qrCodeService;

        public ContextPartService(ILogger<ContextPartService> logger, IQrCodeService qrCodeService, QrDbContext context)
        {
            _logger = logger;
            _qrCodeService = qrCodeService;
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

            // Generate a unique QR token
            var uniqueQrToken = GenerateUniqueQRToken();

            var qrCodeImage = _qrCodeService.GenerateQrCode(uniqueQrToken); //create QR code image based on the token
            if (qrCodeImage == null)
            {
                _logger.LogError("Failed to generate QR code image.");
                return null;
            }

            var newPart = new ContextPart
            {
                Name = request.Name,
                QrToken = uniqueQrToken,
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

            var contextPart = _context.ContextParts.QrTokenExist(qrToken).FirstOrDefaultAsync();

            _logger.LogInformation($"Scanning QR token: {qrToken}. ContextPart found: {contextPart != null}");

            //return  ContextPartResponse.FromEntity(contextPart);

            return await _context.ContextParts
                    .Where(cp => cp.QrToken == qrToken)
                    .Select(cp => ContextPartResponse.FromEntity(cp))
                    .FirstOrDefaultAsync();
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

            _context.Remove(id);
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
        public async Task<IEnumerable<ContextPartResponse>> GetAllContextPartsAsync(Guid contextId)
        {
            var contextParts = await _context.ContextParts
                .Where(cp => cp.Id == contextId)
                .Include(cp => cp.UserTabs.Where(ut => ut.Status == TabStatus.Open))
                .ToListAsync();

            return contextParts.Select(cp => ContextPartResponse.FromEntity(cp));
        }
    }
}
