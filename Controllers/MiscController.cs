using archolosDotNet.Models.Extensions;
using archolosDotNet.Models.Item.Miscellaneous;
using archolosDotNet.Models.Pagination;
using archolosDotNet.Models.Payload;
using archolosDotNet.Models.UserNS;
using archolosDotNet.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiscController(IMiscService service) : ControllerBase
    {
        private readonly IMiscService MiscService = service;

        [HttpGet]
        public Task<PagedResult<Misc>> GetAll([FromBody] ListPayload<MiscFilter> data)
        {
            return MiscService.GetAll(data.filter).toPagedResultAsync(data.pagination);
        }

        [HttpGet("{id}")]
        public ActionResult<Misc> Get(int id)
        {
            var item = MiscService.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult<Misc> Create(Misc data)
        {
            try
            {
                return Ok(MiscService.Create(data));
            }
            catch (Exception e)
            {
                return UnprocessableEntity(new { message = "Invalid data", originalError = e });
            }
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Update(Misc data)
        {
            try
            {
                var result = MiscService.Update(data);

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
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Delete(int id)
        {
            var result = MiscService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
