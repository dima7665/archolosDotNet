using System.Runtime.Serialization;
using archolosDotNet.EF;
using archolosDotNet.Models;
using archolosDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Newtonsoft.Json;

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
        public ActionResult<List<BaseItem>> GetAll()
        {
            return _context.Items.ToList();

            // працює, в Yaak приходить обєкт(чи список об'єктів) із зміненим енумом, але тип повернення у цьому методі string
            // return JsonConvert.SerializeObject(Item);
            // return JsonConvert.SerializeObject(_context.Items.ToList())
        }

        [HttpGet("{id}")]
        public ActionResult<BaseItem> Get(int id)
        {
            var item = _context.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public IActionResult Create(BaseItemDto _item)
        {
            var item = new BaseItem(_item);
            _context.Items.Add(item);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Create), new BaseItemDto(item)
            {
                id = item.id
            }, item);
        }

        [HttpPut]
        public IActionResult Update(BaseItem data)
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
