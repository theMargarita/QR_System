namespace Application.IServices
{
    public interface IQrCodeGererator
    {
        IQrCodeGererator Create();
        byte[] GenerateQrCode(string content, int width = 250, int height = 250);

    }
}
