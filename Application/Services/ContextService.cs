using Application.DTOs.ContextFolder.Request;
using Application.DTOs.ContextFolder.Response;
using Application.IServices;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ContextService : IContextService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        //private readonly QrCodeService _qrCodeService;

        public ContextService(IUnitOfWork unitOfWork, /*IMapper mapper,*/ ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            //_mapper = mapper;
            _logger = logger;
            //_qrCodeService = qrCodeService;
        }

        public Task<bool> ActivateContextAsync(int contextId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ContextExistsAsync(int contextId)
        {
            throw new NotImplementedException();
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
        //    while (_unitOfWork.Contexts.QRTokenExistsAsync(qrToken).Result);
        //    return qrToken;
        //}

        public async Task<QrContextPartRequest?> CreateContextAsync(QrContextPartRequest newContext)
        {
            //var uniqueToken = GenerateUniqueQRToken();

            var contextEntity = new Context
            {
                Name = newContext.Name,
                QrToken = newContext.QrToken,
                OwnerId = newContext.OwnerId,
                ContextPartIsUnique = newContext.ContextPartIsUnique
            };

            await _unitOfWork.Contexts.CreateAsync(contextEntity);
            await _unitOfWork.SaveChangesAsync();

            if (contextEntity == null)
            {
                _logger.LogError("Failed to create context entity from request.");
                if (contextEntity.QrToken == null)
                {
                    _logger.LogWarning("QrToken is null in the create context request.");
                    return null;
                }
                return null;
            }
            //newContext.QrToken = uniqueToken;

            return newContext;
        }


        public async Task<IEnumerable<ContextResponse>> GetAllContextsAsync()
        {
            var contexts = await _unitOfWork.Contexts.GetAllAsync();

            return contexts.Select(context => new ContextResponse
            {
                //will use automapper later
                Id = context.Id,
                Name = context.Name,
                QrToken = context.QrToken,
                IsActive = context.IsActive,
                OwnerId = context.OwnerId,
                OwnerName = context.Owner?.Name,
                ContextPartIsUnique = context.ContextPartIsUnique,
            });
        }

        public async Task<ContextResponse?> GetContextByIdAsync(int id)
        {
            var contextId = await _unitOfWork.Contexts.GetByIdAsync(id);

            if (contextId == null)
            {
                _logger.LogWarning($"Context with ID {id} not found.");
                return null;
            }

            return new ContextResponse
            {
                Id = contextId.Id,
                Name = contextId.Name,
                QrToken = contextId.QrToken,
                IsActive = contextId.IsActive,
                OwnerId = contextId.OwnerId,
                OwnerName = contextId.Owner?.Name,
                ContextPartIsUnique = contextId.ContextPartIsUnique,
                //ContextPartsid = contextId.Parts.Select(p => p.Id).ToList(),
                //PartName = contextId.ContextPart?.Name
            };
        }

        //public async Task<ContextResponse?> GetContextPartByQrTokenAsync(string qrToken)
        //{
        //    var getQr = await _unitOfWork.Contexts.GetByQrCodeTokenAsync(qrToken);

        //    if (getQr == null)
        //    {
        //        _logger.LogWarning($"Context with QR Token {qrToken} not found.");
        //        return null;
        //    }

        //    return new ContextResponse
        //    {
        //        Id = getQr.Id,
        //        Name = getQr.Name,
        //        QrToken = getQr.QrToken,
        //        IsActive = getQr.IsActive,
        //        OwnerId = getQr.OwnerId,
        //        OwnerName = getQr.Owner?.Name,
        //        ContextPartIsUnique = getQr.ContextPartIsUnique,
        //        //ContextPartsid = getQr.Parts.Select(p => p.Id).ToList(),
        //        //PartName = getQr.ContextPart?.Name
        //    };
        //}

        public async Task<bool> IsContextActiveAsync(int contextId)
        {
            var context = await _unitOfWork.Contexts.GetByIdAsync(contextId);
            if (context == null)
            {
                _logger.LogWarning($"Context with ID {contextId} not found.");
                return false;
            }

            return context.IsActive;
        }

        public async Task<bool> RemoveContextAsync(int id)
        {
            var findId = _unitOfWork.Contexts.GetByIdAsync(id);
            if (findId == null)
            {
                _logger.LogWarning($"Context with ID {id} not found for deletion.");
                return false;
            }
            await _unitOfWork.Contexts.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Context with ID {id} has been deleted.");
            return true;
        }
    }
}
