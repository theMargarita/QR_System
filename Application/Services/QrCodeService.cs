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
        private static int _colorIndex = 0;
        private static readonly object _lock = new object();
        // Definiera en färgpalett
        private static readonly Color[] ColorPalette = new Color[]
        {
            Color.FromArgb(59, 130, 246),   // Blå
            Color.FromArgb(239, 68, 68),    // Röd
            Color.FromArgb(34, 197, 94),    // Grön
            Color.FromArgb(168, 85, 247),   // Lila
            Color.FromArgb(251, 146, 60),   // Orange
            Color.FromArgb(236, 72, 153),   // Rosa
            Color.FromArgb(20, 184, 166),   // Turkos
            Color.FromArgb(245, 158, 11),   // Gul/Guld
        };
        public QrCodeService(IConfiguration config)
        {
            _config = config;
        }

        public byte[] GenerateQrCode(string qrText, int pixelSize = 300)
        {
            string url = GetQRCodeUrl(qrText);

            Color qrColor = GetNextColor();

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

        private Color GetNextColor()
        {
            lock (_lock)
            {
                Color color = ColorPalette[_colorIndex];
                _colorIndex = (_colorIndex + 1) % ColorPalette.Length; // Loopar tillbaka till början av paletten
                return color;
            }
        }
    }
}
