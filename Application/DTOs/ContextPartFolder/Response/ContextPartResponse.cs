using Application.DTOs.UserTabFolder.Resonse;

namespace Application.DTOs.CPFolder.Response
{
    public record ContextPartResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string ? QrToken { get; init; }

        // nav prop
        public int ContextId { get; init; }
        public string? ContextName { get; init; }  

        // Status
        public bool IsActive { get; init; }
        public int ActiveUserCount { get; init; }
        public List<UserTabResponse> UserTabs { get; init; } = new List<UserTabResponse>();
    }
}
