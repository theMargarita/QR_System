namespace Application.DTOs.UserTabFolder.Request
{
    public record OpenTabRequest
    {
        public string QrToken { get; init; } = string.Empty;
        public Guid UserId { get; init; }
    }
}
