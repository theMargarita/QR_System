using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTabController : ControllerBase
    {
        private readonly IUserTabService _tabService;
        //private readonly 

    }
}
