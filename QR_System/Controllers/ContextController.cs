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
        private readonly IContextService _contextService;

        public ContextController(IContextService contextService)
        {
            _contextService = contextService;
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

            // Return only token/id — frontend genererar QR-bilden
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
                    context.ContextPartIsUnique
                });
        }

        [HttpGet("{id}/fetchContextById")]
        public async Task<ActionResult<ContextResponse>> GetContextById(Guid id)
        {
            var context = await _contextService.GetContextByIdAsync(id);
            if (context == null) return NotFound($"Context with ID {id} not found.");
            return Ok(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContextResponse>>> GetAllContexts()
        {
            var contexts = await _contextService.GetAllContextsAsync();
            return Ok(contexts);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ContextResponse>>> SearchContexts([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return BadRequest("Search term cannot be empty.");
            var contexts = await _contextService.SearchContext(searchTerm);
            return Ok(contexts);
        }

        // Optional: return token/scan-url instead of PNG (frontend builds QR)
        [HttpGet("{id}/qr")]
        public async Task<IActionResult> GetQrToken(Guid id)
        {
            var context = await _contextService.GetContextByIdAsync(id);
            if (context == null) return NotFound("Context not found.");

            return Ok(new
            {
                context.Id,
                context.QrToken
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContext(Guid id)
        {
            var result = await _contextService.RemoveContextAsync(id);
            if (!result) return NotFound($"Context with ID {id} not found.");
            return NoContent();
        }
    }
}