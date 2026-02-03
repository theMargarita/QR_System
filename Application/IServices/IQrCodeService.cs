namespace Application.IServices
{
    public interface IQrCodeService
    {
        byte[] GenerateQrCode(string qrText, int pixelSize = 300);
        string GenerateQrCodeBase64(string qrText, int pixelSize = 300);
        string GetQRCodeUrl(string qrCodeToken);

    }
}
