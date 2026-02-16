using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContextPartController : ControllerBase
    {
        private readonly IContextPartService _contextPartService;
        private readonly IQrCodeService _qrCodeService;

        public ContextPartController(
            IContextPartService contextPartService,
            IQrCodeService qrCodeService)
        {
            _contextPartService = contextPartService;
            _qrCodeService = qrCodeService;
        }

        [HttpPost]
        public async Task<ActionResult<ContextPartResponse>> CreateContextPart(
            [FromBody] CreateContexPartRequest request)
        {
            var contextPart = await _contextPartService.CreateContextPartAsync(request);

            if (contextPart == null)
            {
                return BadRequest("ContextPart could not be created. Context might not exist.");
            }

            string qrBase64 = _qrCodeService.GenerateQrCodeBase64(contextPart.QrToken);
            var newCp = new
            {
                contextPart.Id,
                contextPart.Name,
                contextPart.QrToken,
                contextPart.IsActive,
                //contextPart.ContextId,
                QrCodeBase64 = qrBase64
            };

            return CreatedAtAction(
                nameof(GetContextPartById),
                new { id = contextPart.Id }, newCp);
        }

        [HttpGet("GetAllContextParts")]
        public async Task<ActionResult<IEnumerable<ContextPartResponse>>> FetchAllContextParts()
        {
            var cp = await _contextPartService.GetAllContextPartsAsync();
            return Ok(cp);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContextPartResponse>> GetContextPartById(Guid id)
        {
            var contextPart = await _contextPartService.GetContextPartByIdAsync(id);

            if (contextPart == null)
            {
                return NotFound($"ContextPart with ID {id} not found.");
            }

            return Ok(contextPart);
        }

        [HttpGet("context/{contextId}")]
        public async Task<ActionResult<IEnumerable<ContextPartResponse>>> GetContextParts(
            Guid contextId)
        {
            var contextParts = await _contextPartService.GetAllContextPartWithContxtId(contextId);
            return Ok(contextParts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContextPart(Guid id)
        {
            var result = await _contextPartService.RemoveContextPartAsync(id);

            if (!result)
            {
                return NotFound($"ContextPart with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpGet("{id}/qr/view")]
        public async Task<ContentResult> ViewQrCode(Guid id)
        {
            var contextPart = await _contextPartService.GetContextPartByIdAsync(id);

            if (contextPart == null)
                return Content("<h1>Table not found</h1>", "text/html");

            string qrBase64 = _qrCodeService.GenerateQrCodeBase64(contextPart.QrToken);
            string qrUrl = _qrCodeService.GetQRCodeUrl(contextPart.QrToken);

            string html = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>{contextPart.Name} - QR Code</title>
                <style>
                    body {{ 
                        font-family: Arial, sans-serif; 
                        text-align: center; 
                        margin: 50px;
                        background: #f5f5f5;
                    }}
                    .container {{
                        background: white;
                        padding: 30px;
                        border-radius: 10px;
                        box-shadow: 0 2px 10px rgba(0,0,0,0.1);
                        display: inline-block;
                    }}
                    .qr {{ 
                        border: 3px solid #333; 
                        padding: 20px; 
                        background: white;
                        border-radius: 5px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>{contextPart.Name}</h1>
                    <h3>Context: {contextPart.ContextName}</h3>
                    <div class='qr'>
                        <img src='data:image/png;base64,{qrBase64}' alt='QR Code' />
                    </div>
                    <p><strong>Token:</strong> {contextPart.QrToken}</p>
                    <p><strong>Active Users:</strong> {contextPart.ActiveUserCount}</p>
                    <a href='/api/contextpart/{id}/qr/download' 
                       style='display:inline-block;margin-top:20px;padding:10px 20px;
                              background:#007bff;color:white;text-decoration:none;border-radius:5px;'>
                        Download PNG
                    </a>
                </div>
            </body>
            </html>";

            return Content(html, "text/html");
        }

        [HttpGet("{id}/qr/download")]
        public async Task<IActionResult> DownloadQrCode(Guid id)
        {
            var contextPart = await _contextPartService.GetContextPartByIdAsync(id); //cp id

            if (contextPart == null)
                return NotFound("ContextPart not found.");

            byte[] qrBytes = _qrCodeService.GenerateQrCode(contextPart.QrToken);

            return File(
                qrBytes,
                "image/png",
                $"QR_{contextPart.ContextName}_{contextPart.Name}_{DateTime.Now:yyyyMMdd}.png");
        }
    }
}