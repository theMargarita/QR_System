using Application.DTOs.ContextFolder.Request;
using Application.DTOs.ContextFolder.Response;
using Application.DTOs.CPFolder.Response;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContextController : ControllerBase
    {
        private readonly IContextService _contextService;
        private readonly IQrCodeService _qrCodeService;

        public ContextController(IContextService contextService, IQrCodeService qrCodeService)
        {
            _contextService = contextService;
            _qrCodeService = qrCodeService;
        }

        [HttpPost("CreateContext")]
        [ProducesResponseType(typeof(ContextResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContextResponse>> CreateContext(
            [FromBody] CreateContextRequest request)
        {
            var context = await _contextService.CreateContextAsync(request);

            if (context == null)
            {
                return BadRequest("Context could not be created. Owner might not exist.");
            }

            // Generera QR-kod
            string qrBase64 = _qrCodeService.GenerateQrCodeBase64(context.QrToken);

            return CreatedAtAction(
                nameof(GetContextById),
                new { id = context.Id },
                new
                {
                    context.Id,
                    context.Name,
                    context.QrToken,
                    context.IsActive,
                    context.OwnerId,
                    context.ContextPartIsUnique,
                    QrCodeBase64 = qrBase64
                });
        }

        [HttpGet("{id}/fetchContextById")]
        [ProducesResponseType(typeof(ContextResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContextResponse>> GetContextById(Guid id)
        {
            var context = await _contextService.GetContextByIdAsync(id);

            if (context == null)
            {
                return NotFound($"Context with ID {id} not found.");
            }

            return Ok(context);
        }

        //for admin   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ContextResponse>>> GetAllContexts()
        {
            var contexts = await _contextService.GetAllContextsAsync();
            return Ok(contexts);
        }

     
        [HttpGet("search")]
        [ProducesResponseType(typeof(ContextResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ContextResponse>>> SearchContexts(
            [FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var contexts = await _contextService.SearchContext(searchTerm);
            return Ok(contexts);
        }

        [HttpGet("{id}/qr/view")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ContentResult> ViewQrCode(Guid id)
        {
            var context = await _contextService.GetContextByIdAsync(id);

            if (context == null)
                return Content("<h1>Context not found</h1>", "text/html");

            string qrBase64 = _qrCodeService.GenerateQrCodeBase64(context.QrToken);
            string qrUrl = _qrCodeService.GetQRCodeUrl(context.QrToken);

            string html = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>{context.Name} - QR Code</title>
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
                        display: inline-block;
                        background: white;
                        border-radius: 5px;
                    }}
                    .info {{
                        margin-top: 20px;
                        text-align: left;
                    }}
                    .download-btn {{
                        display: inline-block;
                        margin-top: 20px;
                        padding: 10px 20px;
                        background: #007bff;
                        color: white;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    .download-btn:hover {{
                        background: #0056b3;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>{context.Name}</h1>
                    <div class='qr'>
                        <img src='data:image/png;base64,{qrBase64}' alt='QR Code' />
                    </div>
                    <div class='info'>
                        <p><strong>Token:</strong> {context.QrToken}</p>
                        <p><strong>Scan URL:</strong> {qrUrl}</p>
                        <p><strong>Status:</strong> {(context.IsActive ? "Active" : "Inactive")}</p>
                        {(context.IsTemporary ? $"<p><strong>Event Period:</strong> {context.StartsAt} - {context.ExpiresAt}</p>" : "")}
                    </div>
                    <a href='/api/context/{id}/qr/download' class='download-btn'>Download PNG</a>
                </div>
            </body>
            </html>";

            return Content(html, "text/html");
        }

      
        [HttpGet("{id}/qr/download")]
        [ProducesResponseType(typeof(ContextResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DownloadQrCode(Guid id)
        {
            var context = await _contextService.GetContextByIdAsync(id);

            if (context == null)
                return NotFound("Context not found.");

            byte[] qrBytes = _qrCodeService.GenerateQrCode(context.QrToken);

            return File(
                qrBytes,
                "image/png",
                $"QR_{context.Name.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.png");
        }

      
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ContextResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteContext(Guid id)
        {
            var result = await _contextService.RemoveContextAsync(id);

            if (!result)
            {
                return NotFound($"Context with ID {id} not found.");
            }

            return NoContent();
        }

    }
}