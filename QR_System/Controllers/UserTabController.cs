using Application.DTOs.UserTabFolder.Request;
using Application.DTOs.UserTabFolder.Response;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTabController : ControllerBase
    {
        private readonly IUserTabService _tabService;

        public UserTabController(IUserTabService tabService)
        {
            _tabService = tabService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<UserTabResponse>> GetAllTabs()
        {
            var tabs = await _tabService.GetAllAsync();

            return Ok(tabs);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserTabResponse?>> GetTab(Guid id)
        {
            var tab = await _tabService.GetTabByIdAsync(id);
            if (tab == null) return NotFound();
            return Ok(tab);
        }

        [HttpPost("open")]
        public async Task<ActionResult<UserTabResponse>> OpenTab([FromBody] OpenTabRequest req)
        {
            if (req == null)
            {
                ModelState.AddModelError("req", "Request body is required.");
                return ValidationProblem(ModelState);
            }

            if (string.IsNullOrWhiteSpace(req.QrToken))
            {
                ModelState.AddModelError("qrToken", "qrToken is required.");
                return ValidationProblem(ModelState);
            }

            var tab = await _tabService.GetOrCreateTabByQrTokenAsync(req.QrToken, req.UserId, req.ContextPartId);
            if (tab == null) return NotFound();

            return Ok(tab);
        }
    }
}
