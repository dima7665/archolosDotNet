using archolosDotNet.EF;
using archolosDotNet.Models;
using archolosDotNet.Models.Item.Consumable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumableController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext dbContext = context;

        [HttpGet]
        public ActionResult<List<BaseItem>> GetAll()
        {
            return dbContext.Items.Where(i => i.type == ItemType.Food || i.type == ItemType.Potion).Include(i => i.consumableStats).ToList();
        }

        // [HttpGet("{id}")]
        // public ActionResult<ConsumableStat> Get(int id)
        // {
        //     var item = dbContext.Items.Find(id);

        //     if (item == null)
        //     {
        //         return NotFound();
        //     }

        //     return item;
        // }

        [HttpPost]
        public async Task<IActionResult> Create(ConsumableDto data)
        {
            Console.WriteLine("CREATE");
            var stats = data.consumableStats;

            if (stats == null || stats.Count < 1)
            {
                return BadRequest();
            }

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                // Create item
                dbContext.Items.Add(data);
                await dbContext.SaveChangesAsync();

                var item = dbContext.Items.Find(data.id);

                // Add stats to created item
                foreach (ConsumableStatDto stat in stats)
                {
                    item?.consumableStats.Add(stat.toStat(data.id));
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(data);
            }
            catch
            {
                return UnprocessableEntity();
            }
        }

        // [HttpPut]
        // public IActionResult Update(ConsumableStat data)
        // {
        //     var item = dbContext.ConsumableStats.FirstOrDefault(i => i.id == data.id);

        //     if (item == null)
        //     {
        //         return NotFound();
        //     }

        //     // dbContext.SaveChanges();

        //     return Ok();
        // }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = dbContext.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            dbContext.Items.Remove(item);
            dbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("stats")]
        public ActionResult<List<ConsumableStat>> GetAllStats()
        {
            return dbContext.ConsumableStats.ToList();
        }

        // [HttpPost("stats")]
        // public ActionResult<ConsumableStat> CreateStat(ConsumableStat data)
        // {
        //     dbContext.ConsumableStats.Add(data);
        //     dbContext.SaveChanges();
        //     return Ok(data);
        // }

        [HttpDelete("stats/{id}")]
        public IActionResult DeleteStat(int id)
        {
            var item = dbContext.ConsumableStats.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            dbContext.ConsumableStats.Remove(item);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
