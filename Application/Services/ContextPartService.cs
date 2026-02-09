using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;
using Application.DTOs.UserTabFolder;
using Application.IServices;
using Domain.IRepositories;
using Domian.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ContextPartService : IContextPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContextPartService> _logger;
        private readonly IQrCodeService _qrCodeService;

        public ContextPartService(IUnitOfWork unitOfWOrk, ILogger<ContextPartService> logger, IQrCodeService qrCodeService)
        {
            _unitOfWork = unitOfWOrk;
            _logger = logger;
            _qrCodeService = qrCodeService;
        }

        //might need to change this one late - will test something first
        public async Task<CreateContexPartRequest?> CreateContextPartAsync(CreateContexPartRequest request)
        {
            // Generate a unique QR token
            var uniqueQrToken = GenerateUniqueQRToken();

            // Retrieve the associated context
            var context = await _unitOfWork.Contexts.GetByIdAsync(request.ContextId);

            if (context == null)
            {
                _logger.LogWarning($"Context with ID {request.ContextId} not found.");
            }

            var newPart = new ContextPart
            {
                Name = request.Name,
                QrToken = uniqueQrToken,
                IsActive = request.IsActive,
                Context = context
            };

            var qrCodeImage = _qrCodeService.GenerateQrCode(uniqueQrToken);

            if(newPart == null)
            {
                _logger.LogError("Failed to create new ContextPart instance.");
                return null;
            }

            await _unitOfWork.ContextParts.CreateAsync(newPart);
            await _unitOfWork.SaveChangesAsync();

            return new CreateContexPartRequest
            {
                Name = newPart.Name,
                QrToken = newPart.QrToken,
                IsActive = newPart.IsActive,
                ContextId = newPart.Context?.Id ?? 0
            };
        }

        public string GenerateUniqueQRToken()
        {
            string qrToken;
            do
            {
                //generate a random token
                qrToken = $"CTX_{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
            }
            while (_unitOfWork.ContextParts.QRTokenExistsAsync(qrToken).Result);
            return qrToken;
        }

        public async Task<int> GetActiveUserCountAsync(int contextPartId)
        {
            var count = await _unitOfWork.ContextParts.GetActiveUserCountAsync(contextPartId);

            if (count < 0)
            {
                _logger.LogError($"Failed to retrieve active user count for ContextPart ID {contextPartId}");
                return 0;
            }

            return count;
        }

        // for scanning QR code and retrieving context part details ? 
        public async Task<ContextPartResponse?> GetContextPartByQrTokenAsync(string qrToken)
        {
            var contextPart = await _unitOfWork.ContextParts.GetByQrTokenAsync(qrToken);

            if (contextPart == null)
            {
                _logger.LogWarning($"No ContextPart found for QR token {qrToken}");
                return null;
            }

            return new ContextPartResponse
            {
                ContextId = contextPart.Context.Id,
                ContextName = contextPart.Context.Name,
                Id = contextPart.Id,
                IsActive = contextPart.IsActive,
                Name = contextPart.Name,
                QrToken = contextPart.QrToken,
                ActiveUserCount = contextPart.UserTabs?.Count(ut => !ut.IsClosed) ?? 0
            };
        }

        public async Task<ContextPartResponse?> GetUserActiveTabAsync(int contextPartId, int userId)
        {
            var activeTab = await _unitOfWork.ContextParts.GetUserActiveTabAsync(contextPartId, userId);
            if (activeTab == null)
            {
                _logger.LogInformation($"No active tab found for user ID: {userId} in ContextPart ID: {contextPartId}");
                return null;
            }

            return new ContextPartResponse
            {
                ContextId = activeTab.ContextId,
                Id = activeTab.ContextPartId ?? 0,
                Name = activeTab.ContextPart?.Name ?? string.Empty,
                QrToken = activeTab.ContextPart?.QrToken,
                IsActive = activeTab.ContextPart?.IsActive ?? false,
                UserTabs = new List<UserTabResponse>
                {
                    new UserTabResponse
                    {
                        Id = activeTab.Id,
                        UserId = activeTab.UserId,
                        ContextId = activeTab.ContextId,
                        CreatedAt = activeTab.CreatedAt,
                        ClosedAt = activeTab.ClosedAt,
                        Status = activeTab.Status,
                    }
                }
            };
        }

        //tycker inte riktigt att denna är relevant
        public async Task<bool> IsContextPartOccupiedAsync(int contextPartId)
        {
            var contextPart = await _unitOfWork.ContextParts.GetByIdWithDetailsAsync(contextPartId);
            if (contextPart == null)
            {
                _logger.LogWarning($"ContextPart with ID {contextPartId} does not exist.");
                return false;
            }

            var isOccupied = contextPart.UserTabs.Any(ut => !ut.IsClosed);

            return isOccupied;
        }

        public async Task<bool> RemoveContextPartAsync(int contextPartId)
        {
            var contextPart = await _unitOfWork.ContextParts.GetByIdAsync(contextPartId);
            if (contextPart == null)
            {
                _logger.LogWarning("Attempted to remove non-existent ContextPart with ID {ContextPartId}", contextPartId);
                return false;
            }

            await _unitOfWork.ContextParts.DeleteAsync(contextPart.Id);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Successfully removed ContextPart with ID: {contextPartId}");
            return true;
        }

        public async Task<bool> UserHasActiveTabAsync(int contextPartId, int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
                return false;
            }

            // Check if the user has an active tab in the specified context part
            var hasActiveTab = await _unitOfWork.ContextParts.UserHasActiveTabAsync(contextPartId, userId);
            return hasActiveTab;
        }
    }
}
