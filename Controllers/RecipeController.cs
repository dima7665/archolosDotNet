using archolosDotNet.Models.Extensions;
using archolosDotNet.Models.Item.Recipe;
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
        public Task<PagedResult<Recipe>> GetAll([FromBody] ListPayload<RecipeFilter> data)
        {
            return recipeService.GetAll(data.filter).toPagedResultAsync(data.pagination);
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
    }
}
