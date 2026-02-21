using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using archolosDotNet.Models;
using archolosDotNet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Item>> GetAll() => ItemService.GetAll();

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
