using archolosDotNet.EF;
using archolosDotNet.Migrations;
using archolosDotNet.Models.Item.RecipeNS;
using archolosDotNet.Models.SelectNS;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.Item;

public interface IRecipeService
{
    public IQueryable<RecipeShort> GetAll(RecipeFilter? filters);
    public RecipeShort? GetShortById(int id);
    public Recipe? Create(Recipe data);
    public Recipe? Delete(int id);
    public Recipe? Update(Recipe data);
    public IngredientsList GetListOfIngredients();
}

public class RecipeService(ApplicationDbContext context) : IRecipeService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<RecipeShort> GetAll(RecipeFilter? filter)
    {
        var list = dbContext.Recipes.AsQueryable();

        if (filter != null && filter.skill.HasValue)
        {
            list = list.Where(i => i.requirement == filter.skill);
        }

        var shorts = list.Include(r => r.ingredients).ThenInclude(i => i.armor)
            .Include(r => r.ingredients).ThenInclude(i => i.consumable)
            .Include(r => r.ingredients).ThenInclude(i => i.misc)
            .Include(r => r.ingredients).ThenInclude(i => i.weapon)
            .Select(recipe => new RecipeShort
            {
                id = recipe.id,
                name = recipe.name,
                price = recipe.price,
                description = recipe.description,
                additionalInfo = recipe.additionalInfo,
                sources = recipe.sources,
                requirement = recipe.requirement,
                ingredients = recipe.ingredients.Select(i => new RecipeIngredientShort
                {
                    id = i.id,
                    name = getIngredientName(i),
                    quantity = i.quantity,
                    miscId = i.miscId,
                    consumableId = i.consumableId,
                    weaponId = i.weaponId,
                }).ToList(),
            });

        return shorts.AsQueryable();
    }

    public Recipe? GetById(int id)
    {
        return dbContext.Recipes.Include(i => i.ingredients).SingleOrDefault(i => i.id == id);
    }

    public RecipeShort? GetShortById(int id)
    {
        return dbContext.Recipes.Include(r => r.ingredients).ThenInclude(i => i.armor)
            .Include(r => r.ingredients).ThenInclude(i => i.consumable)
            .Include(r => r.ingredients).ThenInclude(i => i.misc)
            .Include(r => r.ingredients).ThenInclude(i => i.weapon)
            .Include(r => r.misc)
            .Include(r => r.consumable)
            .Include(r => r.weapon)
            .Select(recipe => new RecipeShortWithTarget
            {
                id = recipe.id,
                name = recipe.name,
                price = recipe.price,
                description = recipe.description,
                additionalInfo = recipe.additionalInfo,
                sources = recipe.sources,
                requirement = recipe.requirement,
                misc = recipe.misc,
                consumable = recipe.consumable,
                weapon = recipe.weapon,
                ingredients = recipe.ingredients.Select(i => new RecipeIngredientShort
                {
                    id = i.id,
                    name = getIngredientName(i),
                    quantity = i.quantity,
                    miscId = i.miscId,
                    consumableId = i.consumableId,
                    weaponId = i.weaponId,
                }).ToList(),
            }).SingleOrDefault(e => e.id == id);
    }

    public IngredientsList GetListOfIngredients()
    {
        var res = new IngredientsList
        {
            misc = dbContext.Miscs.Select(e => new SelectOption { id = e.id, name = e.name }).ToList(),
            consumables = dbContext.Consumables.Select(e => new SelectOption { id = e.id, name = e.name }).ToList(),
            weapons = dbContext.Weapons.Select(e => new SelectOption { id = e.id, name = e.name }).ToList(),
        };

        return res;
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

        if (item == null)
        {
            return null;
        }

        // // Update base item
        ItemService.updateItem(item, data);
        if (data.miscId != item.miscId) item.miscId = data.miscId;
        if (data.consumableId != item.consumableId) item.consumableId = data.consumableId;
        if (data.weaponId != item.weaponId) item.weaponId = data.weaponId;
        if (data.armorId != item.armorId) item.armorId = data.armorId;

        dbContext.SaveChanges();

        // // Start to update Recipe stats
        var currentIngredients = item.ingredients!.ToList(); // current ingredients
        var payloadIngredients = data.ingredients.ToList(); // ingredients from payload

        // // Remove stats
        foreach (var ci in currentIngredients)
        {
            var curStatUpdateData = payloadIngredients.Find(pi => ci.id == pi.id);

            if (curStatUpdateData == null)
            {
                item.ingredients!.Remove(ci);
            }
        }

        // // Add or update stats
        foreach (var pi in payloadIngredients)
        {
            var existedIngredient = currentIngredients.Find(ci => ci.id == pi.id);

            if (existedIngredient == null)
            {
                item.ingredients!.Add(pi);
            }
            else
            {
                if (existedIngredient.quantity != pi.quantity) existedIngredient.quantity = pi.quantity;
                if (existedIngredient.miscId != pi.miscId) existedIngredient.miscId = pi.miscId;
                if (existedIngredient.consumableId != pi.consumableId) existedIngredient.consumableId = pi.consumableId;
                if (existedIngredient.weaponId != pi.weaponId) existedIngredient.weaponId = pi.weaponId;
                if (existedIngredient.armorId != pi.armorId) existedIngredient.armorId = pi.armorId;
            }
        }

        dbContext.SaveChanges();
        transaction.Commit();

        return item;
    }

    private static string getIngredientName(RecipeIngredient i)
    {
        return i.consumable != null ? i.consumable.name
            : i.misc != null ? i.misc.name
            : i.weapon != null ? i.weapon.name
            : i.armor != null ? i.armor.name
            : "-";
    }
}


/*
    GetAll
    ... 
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
    ...

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
    */


/*
    GetAll resulting query

      SELECT r1.id, r1.name, r1.price, r1.description, r1."additionalInfo", r1.sources, r1.requirement,
             s.id, s."armorId", s."consumableId", s."miscId", s.quantity, s."recipeId", s."weaponId", s.id0,
             s."additionalInfo", s.description, s.name, s.price, s.sources, s.id1, s."additionalInfo0", s.description0, 
             s.name0, s.price0, s.sources0, s.type, s.id2, s."additionalInfo1", s.description1, s.name1, s.price1, s.sources1, 
             s.id3, s."additionalInfo2", s."armorPiercing", s.damage, s."damageType", s.description2, s.name2, s.price2, s.range, 
             s.skill, s."skillBonus", s."skillRequirement", s.sources2, s.type0
      FROM (
          SELECT r.id, r.name, r.price, r.description, r."additionalInfo", r.sources, r.requirement
          FROM "Recipes" AS r
          LIMIT @p1 OFFSET @p
      ) AS r1
      LEFT JOIN (
          SELECT r0.id, r0."armorId", r0."consumableId", r0."miscId", r0.quantity, r0."recipeId", r0."weaponId",
                 a.id AS id0, a."additionalInfo", a.description, a.name, a.price, a.sources,
                 c.id AS id1, c."additionalInfo" AS "additionalInfo0", c.description AS description0, 
                 c.name AS name0, c.price AS price0, c.sources AS sources0, c.type, 
                 m.id AS id2, m."additionalInfo" AS "additionalInfo1", m.description AS description1, 
                 m.name AS name1, m.price AS price1, m.sources AS sources1, 
                 w.id AS id3, w."additionalInfo" AS "additionalInfo2", w."armorPiercing", w.damage, w."damageType", 
                 w.description AS description2, w.name AS name2, w.price AS price2, w.range, w.skill, w."skillBonus", 
                 w."skillRequirement", w.sources AS sources2, w.type AS type0
          FROM "RecipeIngredients" AS r0
          LEFT JOIN "Armors" AS a ON r0."armorId" = a.id
          LEFT JOIN "Consumables" AS c ON r0."consumableId" = c.id
          LEFT JOIN "Miscs" AS m ON r0."miscId" = m.id
          LEFT JOIN "Weapons" AS w ON r0."weaponId" = w.id
      ) AS s ON r1.id = s."recipeId"
      ORDER BY r1.id, s.id, s.id0, s.id1, s.id2
*/