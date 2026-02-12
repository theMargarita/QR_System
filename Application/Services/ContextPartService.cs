using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;
using Application.IServices;
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

        //public async Task<ContextPartResponse?> CreateContextPartAsync(CreateContexPartRequest request)
        //{
        //    // Generate a unique QR token
        //    var uniqueQrToken = GenerateUniqueQRToken();

        //    var context = await _context.Contexts.FindAsync(request.ContextId);

        //    if (context == null)
        //    {
        //        _logger.LogWarning($"Context with ID {request.ContextId} not found.");
        //    }

        //    var newPart = new ContextPart
        //    {
        //        Name = request.Name,
        //        QrToken = uniqueQrToken,
        //        IsActive = request.IsActive,
        //        Context = context
        //    };

        //    var qrCodeImage = _qrCodeService.GenerateQrCode(uniqueQrToken);

        //    _context.ContextParts.Add(newPart);
        //    await _context.SaveChangesAsync();

        //    return ContextPartResponse.FromEntity(newPart);
        //}

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

        public async Task<ContextPartResponse?> GetOrCreateActiveTabAsync(/*Guid cpId, Guid userId*/)
        {
            throw new NotImplementedException();
        }
        //  scanning QR code => go to contextpart dashboard => choose products => add to order == transaction added to usertab 
        public async Task<ContextPartResponse?> ScanQrTokenAsync(string qrToken)
        {
            // Log the incoming request for better traceability
            var contextPart = _context.ContextParts.QrTokenExist(qrToken).FirstOrDefaultAsync();
            _logger.LogInformation($"Scanning QR token: {qrToken}. ContextPart found: {contextPart != null}");

            //create or get an active tab 
            var activeTab = await GetOrCreateActiveTabAsync();

            //return await ContextPartResponse.FromEntity(activeTab);

            return null;
        }

        public async Task<ContextPartResponse?> GetUserActiveTabAsync(Guid contextPartId, Guid userId)
        {
            var activeTab = _context.ContextParts
                .Where(cp => cp.Id == contextPartId)
                .SelectMany(cp => cp.UserTabs)
                .Where(ut => ut.UserId == userId && !ut.IsClosed)
                .Select(ut => new
                {
                    ut.ContextId,
                    ut.ContextPartId,
                    ut.ContextPart
                })
                .ToListAsync();


            return await _context.ContextParts
                .Where(cp => cp.Id == contextPartId)
                .Select(cp => ContextPartResponse.FromEntity(cp))
                .FirstOrDefaultAsync();
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
            var id = await _context.ContextParts.FindAsync(contextPartId);
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

        public Task<ContextPartResponse?> GetOrCreateActiveTabAsync(Guid cpId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ContextPartResponse?> ScanQrTokenAsync(string qrToken, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ContextPartResponse?> CreateContextPartAsync(CreateContexPartRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
