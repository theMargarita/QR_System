using Application.DTOs.OwnerFolder;
using Domain.Models;

namespace QR_System.Controllers
{
    public interface IOwnerService
    {
        Task<OwnerResponse> CreateOwnerAsync(string ownerName);
        Task<bool> RemoveOwnerAsync(Guid ownerId);
        Task<OwnerResponse?> GetOwnerByIdAsync(Guid ownerId);
        Task<IEnumerable<OwnerResponse>> GetAllOwnersAsync();
    }
}
