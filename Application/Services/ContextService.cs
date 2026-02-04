using Application.DTOs.Requests;
using Application.DTOs.Response;
using Application.IServices;

namespace Application.Services
{
    public class ContextService : IContextService
    {
        public Task<CreateContextRequest?> CreateContextAsync(CreateContextRequest newContext)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContextResponse>> GetAllContextsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ContextResponse?> GetContextByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ContextResponse?> GetContextPartByQrTokenAsync(string qrToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveContextAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
