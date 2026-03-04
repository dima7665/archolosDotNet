using archolosDotNet.Models.Extensions;
using archolosDotNet.Models.Pagination;
using archolosDotNet.Models.Payload;
using archolosDotNet.Models.UserNS;
using archolosDotNet.Services.UserNS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service, TokenProvider tokenProvider) : ControllerBase
    {
        private readonly IAuthService authService = service;

        [HttpGet]
        public PagedResult<SimpleUser> GetAll([FromBody] ListPayload<UserFilter> data)
        {
            return authService.GetAll().toPagedResult(data.pagination);
        }

        [HttpPost("create")]
        public ActionResult<UserDto> createUser(UserDto user)
        {
            try
            {
                return Ok(authService.CreateUser(user));
            }
            catch (Exception e)
            {
                if (e.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    return Conflict("User with this email already exist");
                }

                return UnprocessableEntity(new { message = "Invalid data", originalError = e });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = authService.Delete(id);

            return result ? NoContent() : NotFound();
        }

        [HttpGet("token")]
        [Authorize]
        public PagedResult<SimpleUser> GetAll2([FromBody] ListPayload<UserFilter> data)
        {
            return authService.GetAll().toPagedResult(data.pagination);
        }

        [HttpPost("token")]
        public ActionResult<string> GenerateToken(UserDto data)
        {
            if(!authService.IsAuthenticated(data))
            {
                return Unauthorized();
            }

            // var token = jwtService.GenerateToken(data);
            var token = tokenProvider.Create(data);

            return Ok(token);
        }
    }
}
