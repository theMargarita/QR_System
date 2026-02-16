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