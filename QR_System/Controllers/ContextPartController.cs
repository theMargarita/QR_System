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
        private IContextPartService _contextPartService;
        private IQrCodeService _qrCodeService;

        public ContextPartController(IContextPartService contextPartService, IQrCodeService qrCodeService)
        {
            _contextPartService = contextPartService;
            _qrCodeService = qrCodeService;
        }

        //create context part and generate QR code for it
        [HttpPost("createQr")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateContexPartRequest>> CreateContextPart([FromBody] CreateContexPartRequest request)
        {
            var contextPart = await _contextPartService.CreateContextPartAsync(request);

            if (contextPart == null)
            {
                return BadRequest("Context part could not be created.");
            }

            // Generate QR code for the context part's QR token
            string qrBase64 = _qrCodeService.GenerateQrCodeBase64(contextPart.QrToken);
            byte[] qrBytes = _qrCodeService.GenerateQrCode(contextPart.QrToken);

            //byte[] qrBytes = _qrCodeService.GenerateQrCode(contextPart.QrToken);
            return File(qrBytes, "image/png", $"QR_{contextPart.Name}.png"); // returns the QR code as a downloadable PNG file
        }

        //scan QR code and return context part details
        [HttpGet("ScanQr/{qrToken}")]
        [ProducesResponseType(typeof(ContextPartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContextPartResponse>> ScanQrCode(string qrToken)
        {
            var contextToken = await _contextPartService.ScanQrTokenAsync(qrToken);

            if (contextToken == null)
            {
                return NotFound("QR contextToken not found.");
            }

            return Ok(contextToken);
        }

        [HttpGet("{contextPartId:guid}")]
        [ProducesResponseType(typeof(ContextPartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContextPartResponse>> GetContextPartById(Guid contextPartId)
        {
            var contextPart = await _contextPartService.GetContextPartByIdAsync(contextPartId);

            if (contextPart == null)
            {
                return NotFound($"ContextPart with ID {contextPartId} not found.");
            }

            return Ok(contextPart);
        }

        [HttpGet("context/{contextId:guid}")]
        [ProducesResponseType(typeof(List<ContextPartResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ContextPartResponse>>> GetAllContextParts(Guid contextId)
        {
            var contextParts = await _contextPartService.GetAllContextPartsAsync(contextId);

            return Ok(contextParts);
        }

        [HttpGet("{contextPartId:guid}/user-count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetActiveUserCount(Guid contextPartId)
        {
            var count = await _contextPartService.GetActiveUserCountAsync(contextPartId);
            return Ok(count);
        }
    }
}
