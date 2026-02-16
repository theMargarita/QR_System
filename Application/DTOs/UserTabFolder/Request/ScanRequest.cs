namespace Application.DTOs.UserTabFolder.Request
{
    public record ScanRequest
    {
        public string QrToken { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
