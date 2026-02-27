using archolosDotNet.EF;
using archolosDotNet.Models.Item.Enums;
using archolosDotNet.Models.Item.Recipe;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.Item;

public interface IRecipeService
{
    public IQueryable<Recipe> GetAll(RecipeFilter? filters);
    public Recipe? GetById(int id);
    public Recipe? Create(Recipe data);
    public Recipe? Delete(int id);
    public Recipe? Update(Recipe data);
}

public class RecipeService(ApplicationDbContext context) : IRecipeService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<Recipe> GetAll(RecipeFilter? filter)
    {
        var list = dbContext.Recipes.AsQueryable();

        if (filter != null && filter.skill.HasValue)
        {
            list = list.Where(i => i.requirement == filter.skill);
        }

        // -------------------------
        // -------------------------
        // TODO: створити окремі FK ключі від інгредієнта до кожної з таблиць
        // -------------------------
        // -------------------------

        // list = list.Include(i => i.ingredients!).SelectMany;
        // list = list.Include(i => i.ingredients!).OrderBy(s => s.id);


        // .LeftJoin(dbContext.Consumables, i => i.itemId, c => c.id, (ingredient, consumable) => new
        // {
        //     ingredient.id,
        //     ingredient.itemType,
        //     name = consumable.name,
        //     // cName = consumable.name,
        //     ingredient.itemId,
        //     ingredient.quantity
        // })
        // .LeftJoin(dbContext.Weapons, i => i.itemId, w => w.id, (ingredient, weapon) => new
        // {
        //     ingredient.id,
        //     ingredient.itemType,
        //     wName = weapon.name,
        //     ingredient.cName,
        //     ingredient.itemId,
        //     ingredient.quantity
        // })
        // .Select(cr => new
        // {
        //     cr.id,
        //     name = cr.itemType == RecipeItemType.Consumable ? cr.wName : cr.cName,
        //     cr.itemType,
        //     cr.itemId,
        //     cr.quantity
        // })


        // if (filter != null && filter.stat.HasValue)
        // {
        //     list = list.Where(i => i.RecipeStats!.Any(s => s.stat == filter.stat));
        // }

        return list;
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
        var item = dbContext.Recipes.Find(id);

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
