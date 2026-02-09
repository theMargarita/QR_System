using Application.DTOs.ContextFolder.Request;
using Application.DTOs.ContextFolder.Response;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContextController : ControllerBase
    {
        private IContextService _contextService;
        private IQrCodeService _qrCodeService;
        public ContextController(IContextService contextService, IQrCodeService qrCodeService)
        {
            _contextService = contextService;
            _qrCodeService = qrCodeService;
        }

        //should be a response instead a request because it could be nice with an id
        [HttpPost("createContext")]
        public async Task<ActionResult<ContextResponse>> CreateContext([FromBody] QrContextPartRequest newContext)
        {
            var context = await _contextService.CreateContextAsync(newContext);

            if (context == null)
            {
                return BadRequest("Context could not be created.");
            }

            // Generate QR code for the context's QR token
            string qrBase64 = _qrCodeService.GenerateQrCodeBase64(context.QrToken);
            byte[] qrBytes = _qrCodeService.GenerateQrCode(context.QrToken);

            return Ok(new
            {
                //Id = context.I,
                Name = context.Name,
                QrToken = context.QrToken,
                QrCodeBase64 = qrBase64 //can be displayed in frontend
                //url to qr code image if needed?
            });
        }


        //not sure about this one
        [HttpGet("viewQr/{contextId}")]
        public async Task<ContentResult> ViewQR(int contextId)
        {
            var context = await _contextService.GetContextByIdAsync(contextId);

            if (context == null)
                return Content("<h1>Context not found</h1>", "text/html");

            string qrBase64 = _qrCodeService.GenerateQrCodeBase64(context.QrToken);
            string qrUrl = _qrCodeService.GetQRCodeUrl(context.QrToken);

            string html = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>{context.Name} - QR Code</title>
                <style>
                    body {{ font-family: Arial; text-align: center; margin: 50px; }}
                    .qr {{ border: 3px solid #333; padding: 20px; display: inline-block; }}
                </style>
            </head>
            <body>
                <h1>{context.Name}</h1>
                <div class='qr'>
                    <img src='data:image/png;base64,{qrBase64}' alt='QR Code' />
                </div>
                <p><strong>Token:</strong> {context.QrToken}</p>
                <p><strong>Scan leads to:</strong> {qrUrl}</p>
                <p><a href='/api/context/downloadQr/{contextId}'>Download PNG</a></p>
            </body>
            </html>";

            return Content(html, "text/html");
        }

        // this endpoint with be very usable!
        [HttpGet("downloadQr/{contextId}")]
        public async Task<IActionResult> DownloadQR(int contextId)
        {
            var context = await _contextService.GetContextByIdAsync(contextId);

            if (context == null)
                return NotFound();

            byte[] qrBytes = _qrCodeService.GenerateQrCode(context.QrToken);
            return File(qrBytes, "image/png", $"QR_{context.Name}.png");
        }

       
        //[HttpGet("scan/{qrToken}")]
        //public async Task<ActionResult> ScanQR(string qrToken)
        //{
        //    var context = await _contextService.GetContextPartByQrTokenAsync(qrToken);

        //    if (context == null)
        //    {
        //        return NotFound("Context part not found for the provided QR token.");
        //    }

        //    return Ok(
        //        new
        //        {
        //            ContextId = context.Id,
        //            ContextName = context.Name,
        //            ContextPartIsUnique = context.ContextPartIsUnique,
        //            ContextPartsid = context.ContextPartsid,
        //            PartName = context.PartName
        //        }
        //    );
        //}

        //[HttpGet("generateQr/{qrText}")]
        ////[ProducesResponseType(typeof(CreateUserRequest), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status201Created)]

        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult GenerateQrCode(string qrText)
        //{
        //    var qrBytes = _qrCodeService.GenerateQrCode(qrText);
        //    return File(qrBytes, "image/png");
        //}
    }
}

//for future inspiration

//Task<ActionResult<ScanResponse>> ScanQR(string qrToken)
//Task<ActionResult<ContextResponse>> GetContextDetails(int contextId)
//Task<ActionResult<JoinResponse>> JoinContext(int contextId, JoinRequest request) - i think this one is relavent
//Task<ActionResult> LeaveContext(int contextId, int userId)