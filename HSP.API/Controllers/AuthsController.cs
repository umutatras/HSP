using HSP.Core.Dtos;
using HSP.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {

            var result = _authService.GetAll();
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userForLoginDto)
        {
            var userLogin = await _authService.Login(userForLoginDto);
            return Ok(userLogin);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto userForRegisterDto)
        {
            var userRegister = await _authService.Register(userForRegisterDto);

            return Ok(userRegister);
        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var token = _authService.RefreshToken(refreshToken);

            return Ok(token);
        }
    }
}
