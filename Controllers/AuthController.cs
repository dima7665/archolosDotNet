using archolosDotNet.Models.UserNS;
using archolosDotNet.Services.UserNS;
using Microsoft.AspNetCore.Mvc;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult Login(LoginRequest data)
        {
            var result = authService.Login(data);

            if (result == null)
            {
                return NotFound("Wrong email or password");
            }

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public ActionResult LoginWithToken([FromQuery] string token)
        {
            var result = authService.RefreshTokens(token);

            if (result == null)
            {
                return NotFound("Refresh token has been expired");
            }

            return Ok(result);
        }
    }
}
