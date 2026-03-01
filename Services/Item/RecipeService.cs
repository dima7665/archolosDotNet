using System.Threading.Tasks;
using archolosDotNet.EF;
using archolosDotNet.Models.Item.RecipeNS;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.Item;

public interface IRecipeService
{
    public IQueryable<RecipeShort> GetAll(RecipeFilter? filters);
    public Recipe? GetById(int id);
    public Recipe? Create(Recipe data);
    public Recipe? Delete(int id);
    public Recipe? Update(Recipe data);
}

public class RecipeService(ApplicationDbContext context, IConsumableService _consumableService) : IRecipeService
{
    private readonly ApplicationDbContext dbContext = context;
    private readonly IConsumableService consumableService = _consumableService;

    public IQueryable<RecipeShort> GetAll(RecipeFilter? filter)
    {
        var list = dbContext.Recipes.AsQueryable();

        if (filter != null && filter.skill.HasValue)
        {
            list = list.Where(i => i.requirement == filter.skill);
        }

        var shorts = new List<RecipeShort>();

        foreach (Recipe recipe in list.ToList())
        {
            var rawIngredients = dbContext.RecipeIngredients.Where(ri => ri.recipeId == recipe.id).ToList();

            var ingredients = new List<RecipeIngredientShort>();

            foreach (RecipeIngredient ingredient in rawIngredients)
            {
                ingredients.Add(new RecipeIngredientShort
                {
                    id = ingredient.id,
                    name = getIngredientName(ingredient, dbContext),
                    quantity = ingredient.quantity,
                });
            }

            var shortR = new RecipeShort
            {
                id = recipe.id,
                name = recipe.name,
                price = recipe.price,
                description = recipe.description,
                additionalInfo = recipe.additionalInfo,
                sources = recipe.sources,
                requirement = recipe.requirement,
                ingredients = ingredients,
            };

            shorts.Add(shortR);
        }

        return shorts.AsQueryable();
    }

    private string getIngredientName(RecipeIngredient i, ApplicationDbContext context)
    {
        if (i.consumableId.HasValue)
        {
            return consumableService.GetById((int)i.consumableId)!.name;
        }

        if (i.weaponId.HasValue)
        {
            return context.Weapons.SingleOrDefault(w => w.id == i.weaponId)!.name;
        }

        if (i.miscId.HasValue)
        {
            return context.Miscs.SingleOrDefault(m => m.id == i.miscId)!.name;
        }

        return "PLACEHOLDER";
    }

    public Recipe? GetById(int id)
    {
        return dbContext.Recipes.Include(i => i.ingredients).SingleOrDefault(i => i.id == id);
    }

    public Recipe? Create(Recipe data)
    {
        dbContext.Recipes.Add(data);
        dbContext.SaveChanges();

        return data;
    }

    public Recipe? Delete(int id)
    {
        var item = dbContext.Recipes.SingleOrDefault(i => i.id == id);
        Console.WriteLine("DELETE " + item?.id);

        if (item == null)
        {
            return null;
        }

        dbContext.Recipes.Remove(item);
        dbContext.SaveChanges();

        return item;
    }

    public Recipe? Update(Recipe data)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        var item = GetById(data.id);

        // if (item == null)
        // {
        //     return null;
        // }

        // // Update base item
        // ItemService.updateItem(item, data);
        // dbContext.SaveChanges();

        // // Start to update Recipe stats
        // var curStats = item.RecipeStats!.ToList(); // current stats
        // var stats = data.RecipeStats.ToList(); // stats from payload

        // // Remove stats
        // foreach (var cs in curStats)
        // {
        //     var curStatUpdateData = stats.Find(s => cs.stat == s.stat && cs.isPermanent == s.isPermanent);

        //     if (curStatUpdateData == null)
        //     {
        //         item.RecipeStats!.Remove(cs);
        //     }
        // }

        // // Add or update stats
        // foreach (var s in stats)
        // {
        //     var existedStat = curStats.Find(cs => cs.stat == s.stat && cs.isPermanent == s.isPermanent);

        //     if (existedStat == null)
        //     {
        //         item.RecipeStats!.Add(s);
        //     }
        //     else
        //     {
        //         if (existedStat.value != null && existedStat.value != s.value) existedStat.value = s.value;
        //         if (existedStat.duration != null && existedStat.duration != s.duration) existedStat.duration = s.duration;
        //         if (existedStat.isPercentage != s.isPercentage) existedStat.isPercentage = s.isPercentage;

        //     }
        // }

        // dbContext.SaveChanges();
        // transaction.Commit();

        return item;
    }
}
