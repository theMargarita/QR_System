namespace Application.DTOs.UserTabFolder.Request
{
    public record ScanQrCodeRequest
    {
        public string QrToken { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
