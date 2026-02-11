using Application.DTOs.UserTabFolder.Resonse;
using Domian.Models;

namespace Application.DTOs.CPFolder.Response
{
    public record ContextPartResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string ? QrToken { get; init; }

        // nav prop
        public int ContextId { get; init; }
        public string? ContextName { get; init; }  

        // Status
        public bool IsActive { get; init; }
        public int ActiveUserCount { get; init; }
        public List<UserTabResponse> UserTabs { get; init; } = new List<UserTabResponse>();

        public static ContextPartResponse FromEntity(ContextPart cp)
        {
            return new ContextPartResponse
            {
                Id = cp.Id,
                Name = cp.Name,
                QrToken = cp.QrToken,
                ContextId = cp.Context?.Id ?? 0,
                ContextName = cp.Context?.Name,
                IsActive = cp.IsActive,
                ActiveUserCount = cp.UserTabs?.Count(ut => !ut.IsClosed) ?? 0,
                UserTabs = cp.UserTabs?.Where(ut => !ut.IsClosed).Select(ut => new UserTabResponse
                {
                    Id = ut.Id,
                    UserId = ut.UserId,
                    ContextId = ut.ContextId,
                    CreatedAt = ut.CreatedAt,
                    ClosedAt = ut.ClosedAt,
                    //Status = ut.Status
                }).ToList() ?? new List<UserTabResponse>()
            };
        }
}
