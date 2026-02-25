using archolosDotNet.EF;
using archolosDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly ApplicationDbContext dbContext = _context;

        [HttpGet]
        public ActionResult<List<BaseItem>> GetAll()
        {
            return dbContext.Items.ToList();

            // працює, в Yaak приходить обєкт(чи список об'єктів) із зміненим енумом, але тип повернення у цьому методі string
            // return JsonConvert.SerializeObject(Item);
            // return JsonConvert.SerializeObject(_context.Items.ToList())
        }

        [HttpGet("{id}")]
        public ActionResult<BaseItem> Get(int id)
        {
            var item = dbContext.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public IActionResult Create(BaseItem item)
        {
            // var item = new BaseItem(_item);
            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(Create), new
            {
                id = item.id
            }, item);
        }

        [HttpPut]
        public IActionResult Update(BaseItem data)
        {
            var item = dbContext.Items.FirstOrDefault(i => i.id == data.id);

            if (item == null)
            {
                return NotFound();
            }

            if (data.name != null && data.name != item.name) item.name = data.name;
            if (data.price != null && data.price != item.price) item.price = data.price;
            if (data.type != null && data.type != item.type) item.type = data.type;
            if (data.description != null && data.description != item.description) item.description = data.description;
            if (data.additionalInfo != null && data.additionalInfo != item.additionalInfo) item.additionalInfo = data.additionalInfo;

            dbContext.SaveChanges();

            return Ok();

            // або так, але тоді не можна витягнути item і порівнювати значення
            // dbContext.Attach(data);
            // dbContext.Entry(data).Property(p => p.name).IsModified = true;
            // if (data.price != null) dbContext.Entry(data).Property(p => p.price).IsModified = true;
            // if (data.type != null) dbContext.Entry(data).Property("type").IsModified = true;
        }

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
    }
}
