using archolosDotNet.Models.Extensions;
using archolosDotNet.Models.Item.Weapon;
using archolosDotNet.Models.Pagination;
using archolosDotNet.Models.Payload;
using archolosDotNet.Services.Item;
using Microsoft.AspNetCore.Mvc;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController(IWeaponService service) : ControllerBase
    {
        private readonly IWeaponService WeaponService = service;

        [HttpGet]
        public Task<PagedResult<Weapon>> GetAll([FromBody] ListPayload<WeaponFilter> data)
        {
            return WeaponService.GetAll(data.filter).toPagedResultAsync(data.pagination);
        }

        [HttpGet("{id}")]
        public ActionResult<Weapon> Get(int id)
        {
            var item = WeaponService.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public ActionResult<Weapon> Create(Weapon data)
        {
            try
            {
                return Ok(WeaponService.Create(data));
            }
            catch (Exception e)
            {
                return UnprocessableEntity(new { message = "Invalid data", originalError = e });
            }
        }

        [HttpPut]
        public IActionResult Update(Weapon data)
        {
            try
            {
                var result = WeaponService.Update(data);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return UnprocessableEntity(new { message = "Invalid data", originalError = e });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = WeaponService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
