using Application.DTOs.Requests;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetUser")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetUserByPhone(string phoneNumber)
        {
            var user = await _userService.GetUserByPhoneAsync(phoneNumber);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //this endpoint does not work anymore - good to know
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //this one works now as it should
        [HttpPost ("CreateUser")]
        [ProducesResponseType(typeof(CreateUserRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<CreateUserRequest>> CreateUser([FromBody] CreateUserRequest newUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdUser = await _userService.CreateUserAsync(newUser);

            return newUser;
        }

        [HttpDelete]
        [ProducesResponseType(typeof(CreateUserRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _userService.GetUserByIdAsync(id);

            if(id == null)
            {
                return NotFound($"Could not find the use with the Id: {id}");
            }

            await _userService.RemoveUserAsync(id);
            return Ok($"User with id {id} has been deleted.");



        }
    }
}
