using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Models;
using BorrowingSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowingSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<User> GetUserById(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = _userService.CreateUser(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user", error = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public ActionResult<User> UpdateUser(Guid id, [FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedUser = _userService.UpdateUser(id, userDto);
                if (updatedUser == null) return NotFound(new { message = "User not found" });

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the user", error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound(new { message = "User not found" });

            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
