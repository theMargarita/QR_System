//using Application.DTOs.UserTabFolder.AdminTabFolder;
//using Application.DTOs.UserTabFolder.Resonse;
//using Application.IServices;
//using Domain.Models;
//using Infrastructure.Data;
//using Microsoft.Extensions.Logging;

//namespace Application.Services
//{
//    public class UserTabService : IUserTabService
//    {
//        private readonly QrDbContext _context;
//        private readonly ILogger _logger;
//        public UserTabService(ILogger<UserTabService> logger, QrDbContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        public Task<bool> CloseTabAsync(int tabId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<UserTabResponse> CreateAsync(UserTab tab)
//        {
//            var createdTab = await _unitOfWork.UserTabs.CreateAsync(tab);
//            await _unitOfWork.SaveChangesAsync();

//            return new UserTabResponse
//            {
//                Id = createdTab.Id,
//                UserId = createdTab.UserId,
//                ContextId = createdTab.ContextId,
//                CreatedAt = createdTab.CreatedAt,
//                ClosedAt = createdTab.ClosedAt,
//                Status = createdTab.Status.ToString(),
//                ContextName = createdTab.ContextPart.Name,
//                UserFullName = $"{createdTab.User.FirstName} {createdTab.User.LastName}",


//            };
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            var deletedTab = await _unitOfWork.UserTabs.GetByIdAsync(id);

//            if (deletedTab == null)
//            {
//                return false;
//            }
//            await _unitOfWork.UserTabs.DeleteAsync(id);
//            await _unitOfWork.SaveChangesAsync();

//            _logger.LogInformation($"Deleted UserTab with ID {id}");
//            return true;
//        }

//        //for admin dashboard
//        public async Task<IEnumerable<ActiveTabSummary>> GetActiveTabAsync()
//        {
//            var active = await _unitOfWork.UserTabs.GetActiveAsync();

//            var summeries = new List<ActiveTabSummary>(); // build new dt list to return only the required data for the dashboard

//            foreach (var tab in active)
//            {
//                //get the participant count for each active tab
//                var participantCount = await _unitOfWork.UserTabs.GetParticipantCountAsync(tab.Id);

                

//                //get the total amount for each active tab
//                var totalAmount = _unitOfWork.Transactions.GetTotalByTabIdAsync(tab.Id);

//                //get the total paid amount for each active tab
//                var totalPaid = _unitOfWork.Payments.GetTotalPaidAsync(tab.Id);

//                //decimal remainingAmount = totalAmount - totalPaid;
//                var duration = DateTimeOffset.UtcNow - tab.CreatedAt;
//            }


//            var summary = new ActiveTabSummary
//            {
             
//            };

//            summeries.Add(summary);
//            return summeries;
//        }

//        public async Task<IEnumerable<UserTabResponse>> GetAllAsync()
//        {
//            return await _unitOfWork.UserTabs.FindAsync(ut => ut.Status == TabStatus.Open)
//                .ContinueWith(t => t.Result.Select(ut => new UserTabResponse
//                {
//                    //Id = ut.Id,
//                    UserId = ut.UserId,
//                    ContextId = ut.ContextId,
//                    CreatedAt = ut.CreatedAt,
//                    ClosedAt = ut.ClosedAt,
//                    Status = ut.Status.ToString(),
//                    ContextName = ut.ContextPart.Name,
//                    UserFullName = $"{ut.User.FirstName} {ut.User.LastName}",
//                }));
//        }

//        public Task<UserTabResponse?> GetByIdAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<UserDetailTabResponses?> GetTabDetailsAsync(int tabId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> HasOpenTabAsync(int userId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> IsTabPaidAsync(int tabId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<UserTabResponse> UpdateAsync(UserTab tab)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
