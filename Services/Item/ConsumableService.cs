using System.Security.Cryptography;
using archolosDotNet.EF;
using archolosDotNet.Models;
using archolosDotNet.Models.Item.Consumable;
using archolosDotNet.Models.Item.Enums;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.Item;

public interface IConsumableService
{
    public IQueryable<BaseItem> GetAll();
    public BaseItem? GetById(int id);
    public BaseItem? Create(Consumable data);
    public BaseItem? Delete(int id);
    public BaseItem? Update(Consumable data);
}

public class ConsumableService(ApplicationDbContext context) : IConsumableService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<BaseItem> GetAll()
    {
        return dbContext.Items.Where(i => i.type == ItemType.Food || i.type == ItemType.Potion).Include(i => i.consumableStats!.OrderBy(s => s.stat));
    }

    public BaseItem? GetById(int id)
    {
        return dbContext.Items.Include(i => i.consumableStats).SingleOrDefault(i => i.id == id);
    }

    public BaseItem? Create(Consumable data)
    {
        Console.WriteLine("BEFORE transaction");
        using var transaction = dbContext.Database.BeginTransaction();

        // Create item
        dbContext.Items.Add(data);
        Console.WriteLine("ITEM : " + data.type);
        dbContext.SaveChanges();

        Console.WriteLine("After item create");

        var item = dbContext.Items.Find(data.id);

        Console.WriteLine("After item find - " + item != null);

        // Add stats to created item
        foreach (ConsumableStat stat in data.consumableStats)
        {
            item!.consumableStats!.Add(stat.withId(data.id));
        }

        Console.WriteLine("AFTER stats");

        dbContext.SaveChanges();
        transaction.Commit();
        Console.WriteLine("AFTER transaction");

        return item;
    }

    public BaseItem? Delete(int id)
    {
        var item = dbContext.Items.Find(id);

        if (item == null)
        {
            return null;
        }

        dbContext.Items.Remove(item);
        dbContext.SaveChanges();

        return item;
    }

    public BaseItem? Update(Consumable data)
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
