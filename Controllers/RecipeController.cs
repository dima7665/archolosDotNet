using archolosDotNet.Models.Extensions;
using archolosDotNet.Models.Item.RecipeNS;
using archolosDotNet.Models.Pagination;
using archolosDotNet.Models.Payload;
using archolosDotNet.Services.Item;
using Microsoft.AspNetCore.Mvc;

namespace archolosDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController(IRecipeService service) : ControllerBase
    {
        private readonly IRecipeService recipeService = service;

        [HttpGet]
        public PagedResult<RecipeShort> GetAll([FromBody] ListPayload<RecipeFilter> data)
        {
            return recipeService.GetAll(data.filter).toPagedResult(data.pagination);
        }

        [HttpPost]
        public ActionResult<Recipe> Create(Recipe data)
        {
            try
            {
                return Ok(recipeService.Create(data));
            }
            catch (Exception e)
            {
                return UnprocessableEntity(new { message = "Invalid data", originalError = e });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = recipeService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
