namespace Application.DTOs.UserTabFolder.Request
{
    public record OpenTabRequest
    {
        public Guid ContextPartId { get; init; }
        public Guid UserId { get; init; }
        public string QrToken { get; init; } = string.Empty;
    }


    public record OpenTabResponse
    {
        public Guid TabId { get; set; }
        public Guid UserId { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
