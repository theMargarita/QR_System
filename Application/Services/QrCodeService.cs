using Application.IServices;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace Application.Services
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IConfiguration _config;

        public QrCodeService(IConfiguration config)
        {
            _config = config;
        }

        public byte[] GenerateQrCode(string qrText, int pixelSize = 300)
        {
            string url = GetQRCodeUrl(qrText);


            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            using (Bitmap qrCodeImage = qrCode.GetGraphic(20)) //bitmap clean up when pic takes a lot of space 
            using (MemoryStream ms = new MemoryStream()) //Streams måste stängas annars låser de minne
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                return ms.ToArray();//(array av bytes) istället för en fil på disk
            }
        }

        public string GenerateQrCodeBase64(string qrText, int pixelSize = 300)
        {
            byte[] qrCodeBytes = GenerateQrCode(qrText, pixelSize);
            return Convert.ToBase64String(qrCodeBytes);
        }

        public string GetQRCodeUrl(string qrCodeToken)
        {
            //fetch frotnend url from config/appsettings? 
            string frontendBaseUrl = _config["FrontendBaseUrl"] ?? "http://localhost:3000/qr/";
            return $"{frontendBaseUrl}/scan/{qrCodeToken}";
        }
    }
}
