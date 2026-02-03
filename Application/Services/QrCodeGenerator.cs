using QRCoder;

namespace Application.Services
{
    public static class QrCodeGenerator
    {
        //public byte[] GenerateQrCode(string content, int width = 250, int height = 250)
        //{
        //    var qrCode = new QRCoder.QRCodeGenerator();
        //    var qrCodeData = qrCode.CreateQrCode(content, QRCoder.QRCodeGenerator.ECCLevel.Q);
        //    var qrCodeImage = new QRCoder.QRCode(qrCodeData);

        //    using (var bitmap = qrCodeImage.GetGraphic(20))
        //    {
        //        using (var stream = new MemoryStream())
        //        {
        //            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //            return stream.ToArray();
        //        }
        //    }
        //}

        public static void GenerateQrCode(string text, string filePath, int pixelsPerModule = 20)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);

            byte[] qrCodeImage = qrCode.GetGraphic(pixelsPerModule);

            //save t o file
            File.WriteAllBytes(filePath, qrCodeImage);

            //using (var qrCodeImage = qrCode.GetGraphic(pixelsPerModule))
            //{
            //    qrCodeImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            //}
        }
    }
}
