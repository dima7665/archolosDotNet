using archolosDotNet.Models.Extensions;
using archolosDotNet.Models.Item.RecipeNS;
using archolosDotNet.Models.Pagination;
using archolosDotNet.Models.Payload;
using archolosDotNet.Models.UserNS;
using archolosDotNet.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

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

        [HttpGet("{id}")]
        public RecipeShort? GetById(int id)
        {
            return recipeService.GetShortById(id);
        }
        
        [HttpGet("ingredients/select")]
        public IngredientsList GetIngredientsForSelect()
        {
            return recipeService.GetListOfIngredients();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
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
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Delete(int id)
        {
            var result = recipeService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Update(Recipe data)
        {
            var ingredients = data.ingredients;

            if (ingredients == null || ingredients.Count < 1)
            {
                return BadRequest("Recipe should have at least one ingredient");
            }

            try
            {
                var result = recipeService.Update(data);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception e)
            {
                if (e.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    return Conflict("Duplicate ingredients");
                }

                return UnprocessableEntity("Invalid data");
            }
        }
    }
}
