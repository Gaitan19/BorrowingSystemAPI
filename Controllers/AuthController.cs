using AutoMapper;
using BorrowingSystemAPI.DTOs;
using BorrowingSystemAPI.Models;
using BorrowingSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowingSystemAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, IMapper mapper, TokenService tokenService)
        {
            _authService = authService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<AuthDTO> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _authService.Login(loginDto.Email, loginDto.Password);
            if (user == null) return Unauthorized(new { message = "Invalid credentials" });


            var authUser = _mapper.Map<User>(user);

            var token = _tokenService.CreateToken(authUser);

            user.Token = token;

            return Ok(user);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<UserDTO> Register([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = _authService.UserExists(userDto.Email).Result;
            if (userExists) return Conflict(new { message = "User already exists" });

            var newUser = _authService.Register(userDto);
            return Ok(newUser);
        }
    }
}
