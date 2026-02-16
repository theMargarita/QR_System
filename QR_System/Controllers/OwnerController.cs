using Application.DTOs.OwnerFolder;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IContextService _contextService;
        private readonly ILogger<OwnerController> _logger;

        public OwnerController(
            IOwnerService ownerService,
            IContextService contextService,
            ILogger<OwnerController> logger)
        {
            _ownerService = ownerService;
            _contextService = contextService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<OwnerResponse>> CreateOwner(
            [FromBody] OwnerRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Owner name is required.");
            }

            var owner = await _ownerService.CreateOwnerAsync(request.Name);

            return CreatedAtAction(
                nameof(GetOwnerById),
                new { id = owner.Id },
                owner);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerResponse>> GetOwnerById(Guid id)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(id);

            if (owner == null)
            {
                return NotFound($"Owner with ID {id} not found.");
            }

            return Ok(owner);
        }

        [HttpGet("GetAllOwners")]
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetAllOwners()
        {
            var owners = await _ownerService.GetAllOwnersAsync();
            return Ok(owners);
        }

        [HttpGet("{id}/contexts")]
        public async Task<ActionResult> GetOwnerContexts(Guid id)
        {
            // Kontrollera att Owner finns
            var owner = await _ownerService.GetOwnerByIdAsync(id);
            if (owner == null)
            {
                return NotFound($"Owner with ID {id} not found.");
            }

            var allContexts = await _contextService.GetAllContextsAsync();
            var ownerContexts = allContexts.Where(c => c.OwnerId == id);

            return Ok(new
            {
                OwnerId = owner.Id,
                OwnerName = owner.Name,
                ContextCount = ownerContexts.Count(),
                Contexts = ownerContexts
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            // Kontrollera om Owner har några aktiva Contexts
            var allContexts = await _contextService.GetAllContextsAsync();
            var ownerContexts = allContexts.Where(c => c.OwnerId == id).ToList();

            if (ownerContexts.Any())
            {
                return BadRequest(new
                {
                    Error = "Cannot delete owner with active contexts",
                    Message = $"This owner has {ownerContexts.Count} active context(s). Please delete them first.",
                    Contexts = ownerContexts.Select(c => new { c.Id, c.Name })
                });
            }

            var result = await _ownerService.RemoveOwnerAsync(id);

            if (!result)
            {
                return NotFound($"Owner with ID {id} not found.");
            }

            return NoContent();
        }

        // 7. Få statistik för en Owner (dashboard-data)
        [HttpGet("{id}/statistics")]
        public async Task<ActionResult> GetOwnerStatistics(Guid id)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(id);
            if (owner == null)
            {
                return NotFound($"Owner with ID {id} not found.");
            }

            var allContexts = await _contextService.GetAllContextsAsync();
            var ownerContexts = allContexts.Where(c => c.OwnerId == id).ToList();

            var activeContexts = ownerContexts.Count(c => c.IsActive);

            return Ok(new
            {
                Owner = owner,
                Statistics = new
                {
                    TotalContexts = ownerContexts.Count,
                    ActiveContexts = activeContexts,
                    InactiveContexts = ownerContexts.Count - activeContexts,
                },
                RecentContexts = ownerContexts
                    .OrderByDescending(c => c.Id) 
                    .Take(5)
            });
        }
    }
}