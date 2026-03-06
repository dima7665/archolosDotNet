using archolosDotNet.Models.Extensions;
using archolosDotNet.Models.Item.ArmorNS;
using archolosDotNet.Models.Pagination;
using archolosDotNet.Models.Payload;
using archolosDotNet.Models.UserNS;
using archolosDotNet.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmorController(IArmorService service) : ControllerBase
    {
        private readonly IArmorService ArmorService = service;

        [HttpGet]
        public Task<PagedResult<Armor>> GetAll([FromBody] ListPayload<ArmorFilter> data)
        {
            return ArmorService.GetAll(data.filter).toPagedResultAsync(data.pagination);
        }

        [HttpGet("{id}")]
        public ActionResult<Armor> Get(int id)
        {
            var item = ArmorService.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult<Armor> Create(Armor data)
        {
            var stats = data.stats;

            if (stats == null || stats.Count < 1)
            {
                return BadRequest("Armor should have at least one stat");
            }

            try
            {
                return Ok(ArmorService.Create(data));
            }
            catch (Exception e)
            {
                if (e.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    return Conflict("Duplicate stats");
                }

                return UnprocessableEntity(new { message = "Invalid data", originalError = e });
            }
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult<Armor> Update([FromBody] Armor data)
        {
            var stats = data.stats;

            if (stats == null || stats.Count < 1)
            {
                return BadRequest("Armor should have at least one stat");
            }

            try
            {
                var result = ArmorService.Update(data);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                if (e.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    return Conflict("Duplicate stats");
                }

                return UnprocessableEntity("Invalid data");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Delete(int id)
        {
            var result = ArmorService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
