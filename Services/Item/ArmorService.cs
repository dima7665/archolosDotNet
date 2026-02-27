using archolosDotNet.EF;
using archolosDotNet.Models.Item.Armor;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.Item;

public interface IArmorService
{
    public IQueryable<Armor> GetAll(ArmorFilter? filters);
    public Armor? GetById(int id);
    public Armor? Create(Armor data);
    public Armor? Delete(int id);
    public Armor? Update(Armor data);
}

public class ArmorService(ApplicationDbContext context) : IArmorService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<Armor> GetAll(ArmorFilter? filter)
    {
        var list = dbContext.Armors.AsQueryable();

        list = list.Include(i => i.stats!.OrderBy(s => s.type));

        if (filter != null && filter.stat.HasValue)
        {
            list = list.Where(i => i.stats!.Any(s => s.type == filter.stat));
        }

        return list;
    }

    public Armor? GetById(int id)
    {
        return dbContext.Armors.Include(i => i.stats).SingleOrDefault(i => i.id == id);
    }

    public Armor? Create(Armor data)
    {
        dbContext.Armors.Add(data);
        dbContext.SaveChanges();

        return data;
    }

    public Armor? Delete(int id)
    {
        var item = dbContext.Armors.Find(id);

        if (item == null)
        {
            return null;
        }

        dbContext.Armors.Remove(item);
        dbContext.SaveChanges();

        return item;
    }

    public Armor? Update(Armor data)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        var item = GetById(data.id);

        if (item == null)
        {
            return null;
        }

        // Update base item
        ItemService.updateItem(item, data);
        dbContext.SaveChanges();

        // Start to update Armor stats
        var curStats = item.stats!.ToList(); // current stats
        var stats = data.stats.ToList(); // stats from payload

        // Remove stats
        foreach (var cs in curStats)
        {
            var curStatUpdateData = stats.Find(s => cs.type == s.type);

            if (curStatUpdateData == null)
            {
                item.stats!.Remove(cs);
            }
        }

        // Add or update stats
        foreach (var s in stats)
        {
            var existedStat = curStats.Find(cs => cs.type == s.type);

            if (existedStat == null)
            {
                item.stats!.Add(s);
            }
            else
            {
                if (existedStat.value != s.value) existedStat.value = s.value;

            }
        }

        dbContext.SaveChanges();
        transaction.Commit();

        return item;
    }
}
