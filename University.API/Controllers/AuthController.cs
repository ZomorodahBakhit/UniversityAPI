using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using University.Api.Helpers;
using University.API.Filters;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(APIExceptionFilter))]
    public class AuthController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IAuthService _authService;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        public AuthController(IAuthService authService, IJwtTokenHelper jwtTokenHelper, ILogger<StudentController> logger)
        {
            _authService = authService; 
            _jwtTokenHelper = jwtTokenHelper;
            _logger = logger;

        }

        [HttpPost("register")]
        public async Task<ApiResponse> Register([FromBody] RegisterForm form)
        {
            var dto = await _authService.Register(form);
            return new ApiResponse(dto);
        }

        [HttpPost("login")]
        public async Task<ApiResponse> Login([FromBody] LoginForm form)
        {

            var user = await _authService.Login(form);
            var token = _jwtTokenHelper.GenerateToken(user);

            return new ApiResponse(token, StatusCodes.Status200OK);
         

        }



        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var role = User.FindFirstValue(ClaimTypes.Role);

            
            _logger.LogInformation("User accessed /me endpoint. UserID: {UserId}, Role: {Role}, Email: {Email}", userId, role, email);

            return Ok(new
            {
                UserId = userId,
                Email = email,
                Role = role
            });
        }
    }
}
