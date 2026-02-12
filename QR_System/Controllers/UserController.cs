using Application.DTOs.UserFolder.Request;
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

        //kan alltid lägga till GetById senar om det behövs

        [HttpGet("GetUser")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> GetUserByPhone(string searchTerm)
        {
            var user = await _userService.SearchUsersAsync(searchTerm);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        //this one works now as it should
        [HttpPost("CreateUser")]
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
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _userService.GetUserByIdAsync(id);

            if (id == null)
            {
                return NotFound($"Could not find the use with the Id: {id}");
            }

            await _userService.RemoveUserAsync(id);
            return Ok($"User with id {id} has been deleted.");
        }

        //this one does not work 
        [HttpPut("UpdateUser")]
        [ProducesResponseType(typeof(CreateUserRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] CreateUserRequest updatedUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Could not find the user with the Id: {id}");
            }

            await _userService.UpdateUserAsync(id, updatedUser);
            return Ok($"User with id {id} has been updated.");
        }
    }
}
