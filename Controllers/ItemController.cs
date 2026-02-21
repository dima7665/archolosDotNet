using archolosDotNet.EF;
using archolosDotNet.Models;
using archolosDotNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetAll()
        {
            var it = _context.Items.FirstOrDefault();
            Console.WriteLine("--- ", _context.Items.Count());
            if (it == null)
            {
                Console.WriteLine("NO ITEM");
            } else
            {
                Console.WriteLine("ITEM: " + it.Name);
            }

            return ItemService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var item = ItemService.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {

            _context.Items.Add(item);
            _context.SaveChanges();

            ItemService.Create(item);
            return CreatedAtAction(nameof(Create), new { id = item.Id }, item);
        }

        [HttpPut]
        public IActionResult Update(Item data)
        {
            var item = ItemService.Update(data);

            if (item == null)
            {
                return BadRequest();
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = ItemService.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            ItemService.Delete(id);

            return NoContent();
        }
    }
}
