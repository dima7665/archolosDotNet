using archolosDotNet.Models;
using archolosDotNet.Models.Extensions;
using archolosDotNet.Models.Item.Consumable;
using archolosDotNet.Models.Pagination;
using archolosDotNet.Services.Item;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumableController(IConsumableService service) : ControllerBase
    {
        private readonly IConsumableService consumableService = service;

        [HttpGet]
        public Task<PagedResult<BaseItem>> GetAll([FromBody] PaginationPayload data)
        {
            return consumableService.GetAll().toPagedResultAsync(data);
        }

        [HttpGet("{id}")]
        public ActionResult<BaseItem> Get(int id)
        {
            var item = consumableService.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public ActionResult<BaseItem> Create(Consumable data)
        {
            var stats = data.consumableStats;

            if (stats == null || stats.Count < 1)
            {
                return BadRequest("Consumable should have at least one stat");
            }

            try
            {
                return Ok(consumableService.Create(data));
            }
            catch (Exception e)
            {
                if (e.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    return Conflict("Duplicate stats error: check combinations of name and if it is permanent");
                }

                return UnprocessableEntity("Invalid data");
            }
        }

        [HttpPut]
        public IActionResult Update(Consumable data)
        {
            var stats = data.consumableStats;

            if (stats == null || stats.Count < 1)
            {
                return BadRequest("Consumable should have at least one stat");
            }

            try
            {
                var result = consumableService.Update(data);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception e)
            {
                if (e.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    return Conflict("Duplicate stats error: check combinations of name and if it is permanent");
                }

                return UnprocessableEntity("Invalid data");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = consumableService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
