using archolosDotNet.EF;
using archolosDotNet.Models;
using archolosDotNet.Models.Item.Consumable;
using archolosDotNet.Services.Item;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumableController(ApplicationDbContext context, IConsumableService service) : ControllerBase
    {
        private readonly ApplicationDbContext dbContext = context;
        private readonly IConsumableService consumableService = service;

        [HttpGet]
        public ActionResult<List<BaseItem>> GetAll()
        {
            return consumableService.GetAll();
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
        public async Task<ActionResult<BaseItem>> Create(Consumable data)
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

                return Ok(result);
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
