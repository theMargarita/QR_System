using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContextController : ControllerBase
    {
        //private IContextService _contextService;
        //public ContextController(IContextService contextService)
        //{
        //    _contextService = contextService;
        //}

        //contex har med att göra med qr koder och platser

        public Task<ActionResult> ScanQR(string qrToken)
        {
            //var contextPart = await _contextService.GetContextPartByQrTokenAsync(qrToken);
            //if (contextPart == null)
            //{
            //    return NotFound();
            //}
            //return Ok(contextPart);
            return Task.FromResult<ActionResult>(Ok());
        }
    }
}

//for future inspiration

//Task<ActionResult<ScanResponse>> ScanQR(string qrToken)
//Task<ActionResult<ContextResponse>> GetContextDetails(int contextId)
//Task<ActionResult<JoinResponse>> JoinContext(int contextId, JoinRequest request)
//Task<ActionResult> LeaveContext(int contextId, int userId)