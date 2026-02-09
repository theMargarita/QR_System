using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;
using Application.IServices;
using Application.Services;
using Domain.Models;
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

        [HttpPost("createContextPart")]
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
            return File(qrBytes, "image/png", $"QR_{contextPart.Name}.png");

            //return Ok(new
            //{
            //    Name = contextPart.Name,
            //    QrToken = contextPart.QrToken,
            //    IsActive = contextPart.IsActive,
            //    ContextId = contextPart.ContextId,
            //    QrCodeBase64 = qrBase64 //can be displayed in frontend
            //});
        }

        // this endpoint with be very usable!
        //[HttpGet("downloadQr/{contextId}")]
        //public async Task<IActionResult> DownloadQR(int contextId)
        //{
        //    var context = await _contextPartService.(contextId);

        //    if (context == null)
        //        return NotFound();

        //    byte[] qrBytes = _qrCodeService.GenerateQrCode(context.QrToken);
        //    return File(qrBytes, "image/png", $"QR_{context.Name}.png");
        //}

        [HttpGet("GetContextPartId/{contextPartId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ContextPartResponse>> GetContextPartById(int context)
        {


            return Ok(await _contextPartService.IsContextPartOccupiedAsync(context));
        }

    }
}
