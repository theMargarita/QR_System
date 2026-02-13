namespace Application.DTOs.ContextFolder.Request
{
    public record CreateContextRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }
        public string QrToken { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool ContextPartIsUnique { get; set; }

        // För temporära events
        public bool IsTemporary { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
