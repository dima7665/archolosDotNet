using archolosDotNet.Models.UserNS;
using archolosDotNet.Services.UserNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpDelete("logout")]
        [Authorize]
        public ActionResult Logout([FromQuery] string token)
        {
            authService.Logout(token);

            return NoContent();
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
