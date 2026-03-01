using archolosDotNet.EF;
using archolosDotNet.Models.Item.ConsumableNS;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.Item;

public interface IConsumableService
{
    public IQueryable<Consumable> GetAll(ConsumableFilter? filters);
    public Consumable? GetById(int id, bool withStats = true);
    public Consumable? Create(Consumable data);
    public Consumable? Delete(int id);
    public Consumable? Update(Consumable data);
}

public class ConsumableService(ApplicationDbContext context) : IConsumableService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<Consumable> GetAll(ConsumableFilter? filter)
    {
        var list = dbContext.Consumables.AsQueryable();

        if (filter != null && filter.type.HasValue)
        {
            list = list.Where(i => i.type == filter.type);
        }

        list = list.Include(i => i.consumableStats!.OrderBy(s => s.stat));

        if (filter != null && filter.stat.HasValue)
        {
            list = list.Where(i => i.consumableStats!.Any(s => s.stat == filter.stat));
        }

        if (filter != null && filter.isPermanent.HasValue)
        {
            list = list.Where(i => i.consumableStats!.Any(s => s.isPermanent == filter.isPermanent));
        }

        return list;
    }

    public Consumable? GetById(int id, bool withStats = true)
    {
        var consumables = dbContext.Consumables.AsQueryable();;

        if (withStats)
        {
            consumables = consumables.Include(i => i.consumableStats);
        }

        return consumables.SingleOrDefault(i => i.id == id);
    }

    public Consumable? Create(Consumable data)
    {
        dbContext.Consumables.Add(data);
        dbContext.SaveChanges();

        return data;
    }

    public Consumable? Delete(int id)
    {
        var item = dbContext.Consumables.Find(id);

        if (item == null)
        {
            return null;
        }

        dbContext.Consumables.Remove(item);
        dbContext.SaveChanges();

        return item;
    }

    public Consumable? Update(Consumable data)
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

        // Start to update consumable stats
        var curStats = item.consumableStats!.ToList(); // current stats
        var stats = data.consumableStats.ToList(); // stats from payload

        // Remove stats
        foreach (var cs in curStats)
        {
            var curStatUpdateData = stats.Find(s => cs.stat == s.stat && cs.isPermanent == s.isPermanent);

            if (curStatUpdateData == null)
            {
                item.consumableStats!.Remove(cs);
            }
        }

        // Add or update stats
        foreach (var s in stats)
        {
            var existedStat = curStats.Find(cs => cs.stat == s.stat && cs.isPermanent == s.isPermanent);

            if (existedStat == null)
            {
                item.consumableStats!.Add(s);
            }
            else
            {
                if (existedStat.value != null && existedStat.value != s.value) existedStat.value = s.value;
                if (existedStat.duration != null && existedStat.duration != s.duration) existedStat.duration = s.duration;
                if (existedStat.isPercentage != s.isPercentage) existedStat.isPercentage = s.isPercentage;

            }
        }

        dbContext.SaveChanges();
        transaction.Commit();

        return item;
    }
}
