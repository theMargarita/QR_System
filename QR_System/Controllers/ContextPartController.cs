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

        public ContextPartController(IContextPartService contextPartService)
        {
            _contextPartService = contextPartService;
        }

        [HttpPost]
        public async Task<ActionResult<ContextPartResponse>> CreateContextPart([FromBody] CreateContexPartRequest request)
        {
            var contextPart = await _contextPartService.CreateContextPartAsync(request);
            if (contextPart == null) return BadRequest("ContextPart could not be created. Context might not exist.");

            // Return token for frontend to render QR
            var newCp = new
            {
                contextPart.Id,
                contextPart.Name,
                contextPart.QrToken,
                contextPart.IsActive
            };

            return CreatedAtAction(nameof(GetContextPartById), new { id = contextPart.Id }, newCp);
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
            if (contextPart == null) return NotFound($"ContextPart with ID {id} not found.");
            return Ok(contextPart);
        }

        [HttpGet("{id}/qr")]
        public async Task<IActionResult> GetContextPartQrToken(Guid id)
        {
            var contextPart = await _contextPartService.GetContextPartByIdAsync(id);
            if (contextPart == null) return NotFound("ContextPart not found.");
            return Ok(new { contextPart.Id, contextPart.QrToken });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContextPart(Guid id)
        {
            var result = await _contextPartService.RemoveContextPartAsync(id);
            if (!result) return NotFound($"ContextPart with ID {id} not found.");
            return NoContent();
        }
    }
}