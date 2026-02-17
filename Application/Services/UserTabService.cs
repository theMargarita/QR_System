using Application.DTOs.UserTabFolder.AdminTabFolder;
using Application.DTOs.UserTabFolder.Request;
using Application.DTOs.UserTabFolder.Resonse;
using Application.DTOs.UserTabFolder.Response;
using Application.IServices;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserTabService : IUserTabService
    {
        private readonly QrDbContext _context;
        private readonly ILogger _logger;
        public UserTabService(ILogger<UserTabService> logger, QrDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> CloseTabAsync(Guid id)
        {


            throw new Exception();
        }

        public async Task<UserTabResponse?> GetOrCreateTabByQrTokenAsync(string qrToken, Guid userId, Guid cpId)
        {
            var getQr = await _context.ContextParts.Where(x => x.QrToken == qrToken).ToListAsync();
            var ctx = await _context.ContextParts.QrCode(qrToken).FirstOrDefaultAsync();

            if (getQr is null && ctx is null)
            {
                _logger.LogInformation("Could not find");
                return null;
            }

            var newTab = new UserTab
            {
                UserId = userId,
                Status = TabStatus.Open,
                CreatedAt = DateTime.UtcNow,
                ContextPartId = ctx.Id,
            };

            _context.UserTabs.Add(newTab);
            await _context.SaveChangesAsync();

            return new UserTabResponse
            {
                PartId = cpId,
                UserId = userId,
                ContextName = ctx.Name,
                CreatedAt = newTab.CreatedAt,
                ClosedAt = newTab.ClosedAt,
            };
        }

        public async Task<OpenTabResponse?> OpenTabAsync(string qrToken, Guid userId)
        {
            var contextPart = await _context.ContextParts.QrCode(qrToken).FirstOrDefaultAsync();

            if (contextPart == null)
            {
                _logger.LogWarning("No active table found for QR token {Token}", qrToken);
                return null;
            }


            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", userId);
                return null;
            }

            var newTab = new UserTab
            {
                UserId = userId,
                ContextPartId = contextPart.Id,
                ContextId = contextPart.Id,
                Status = TabStatus.Open,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.UserTabs.Add(newTab);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created new tab {TabId} for user {UserId} at {TableName}",
                newTab.Id, userId, contextPart.Name);

            return new OpenTabResponse
            {
                TabId = newTab.Id,
                UserId = userId,
                TableName = contextPart.Name,
                Status = newTab.Status.ToString(),
                CreatedAt = newTab.CreatedAt
            };
        }

        //soft delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var findId = await _context.UserTabs.FindAsync(id);

            if (findId == null)
            {
                _logger.LogInformation($"Could not find anything on the id {findId}");
                return false;
            }
            _context.UserTabs.Remove(findId);
            await _context.SaveChangesAsync();

            return true;
        }

        //active tab for user or just the tab? 
        public Task<IEnumerable<ActiveTabSummary>> GetActiveTabAsync()
        {
            //var tab = _context.UserTabs.ActiveTabsAtTable();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserTabResponse>> GetAllAsync()
        {
            return await _context.UserTabs.Select(x => UserTabResponse.FromBody(x)).ToListAsync();
        }

        public async Task<UserTabResponse?> GetTabByIdAsync(Guid id)
        {
            var utId = await _context.UserTabs.FindAsync(id);
            if (utId == null)
            {
                _logger.LogInformation("Could not find id");
                return null;
            }

            return UserTabResponse.FromBody(utId);
        }

        public async Task<UserDetailTabResponses?> GetTabDetailsAsync(Guid tabId)
        {
            var tab = await _context.UserTabs.FullDetail().FirstOrDefaultAsync();

            if (tab == null)
            {
                _logger.LogInformation($"Could not find tab id {tab}");
                return null;
            }

            return UserDetailTabResponses.FromBody(tab);
        }

        public Task<bool> HasOpenTabAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTabPaidAsync(Guid tabId)
        {
            //var ok = _context.UserTabs.Where(ut =>  ut.Id == tabId).Any();
            throw new Exception();
        }
    }
}
