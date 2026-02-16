using Application.DTOs.UserTabFolder.AdminTabFolder;
using Application.DTOs.UserTabFolder.Request;
using Application.DTOs.UserTabFolder.Resonse;
using Application.DTOs.UserTabFolder.Response;
using Application.IServices;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserTabService : IUserTabService
    {
        private readonly QrDbContext _context;
        private readonly ILogger _logger;
        private readonly IQrCodeService _qrCodeService;
        public UserTabService(ILogger<UserTabService> logger, QrDbContext context, IQrCodeService qrCodeService)
        {
            _logger = logger;
            _context = context;
            _qrCodeService = qrCodeService;
        }

        public async Task<bool> CloseTabAsync(Guid id)
        {

        
            throw new Exception();
        }

        public async Task<UserTabResponse> CreateAsync(Guid contextPartId, Guid userId)
        {
            var cpId = await _context.ContextParts.FindAsync(contextPartId);
            var user = await _context.Users.FindAsync(userId);


            if(cpId == null)
            {
                _logger.LogInformation("Could not find Id ");
                return null;
            }

            var newTab = new UserTab
            {
                ContextPartId = cpId.Id,
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                Status = TabStatus.Open,
            };
            _context.UserTabs.Add(newTab);
            await _context.SaveChangesAsync();

            return UserTabResponse.FromBody(newTab);

        }

        //this one should not reallt remove - it should still exists - ruleset? 
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

        public Task<IEnumerable<ActiveTabSummary>> GetActiveTabAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserTabResponse>> GetAllAsync()
        {
            //var list = _context.UserTabs;
            //return await list.Select(x => new  UserTabResponse.FromUserTab(x)).ToListAsync();

            throw new Exception();
        }

        public Task<UserTabResponse?> GetTabByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailTabResponses?> GetTabDetailsAsync(Guid tabId)
        {
            throw new NotImplementedException();

        }

        public Task<bool> HasOpenTabAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTabPaidAsync(Guid tabId)
        {
            throw new NotImplementedException();
        }

        public Task<ScanRequest> ToOrder(string qrToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserTabResponse> UpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
