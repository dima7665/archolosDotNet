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
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = UserRoles.Super)]
        public PagedResult<SimpleUser> GetAll([FromBody] ListPayload<UserFilter> data)
        {
            return userService.GetAll().toPagedResult(data.pagination);
        }

        [HttpPost("create")]
        [Authorize(Roles = UserRoles.Super)]
        public ActionResult<UserDto> createUser(UserDto user)
        {
            try
            {
                return Ok(userService.CreateUser(user));
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
        [Authorize(Roles = UserRoles.Super)]
        public IActionResult Delete(int id)
        {
            var result = userService.Delete(id);

            return result ? NoContent() : NotFound();
        }
    }
}
