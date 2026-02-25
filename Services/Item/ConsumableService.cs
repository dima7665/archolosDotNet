using System.Security.Cryptography;
using archolosDotNet.EF;
using archolosDotNet.Models;
using archolosDotNet.Models.Item.Consumable;
using archolosDotNet.Models.Item.Enums;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.Item;

public interface IConsumableService
{
    public List<BaseItem> GetAll();
    public BaseItem? GetById(int id);
    public Task<BaseItem?> Create(Consumable data);
    public BaseItem? Delete(int id);
    public Task<BaseItem?> Update(Consumable data);
}

public class ConsumableService(ApplicationDbContext context) : IConsumableService
{
    private readonly ApplicationDbContext dbContext = context;

    public List<BaseItem> GetAll()
    {
        return dbContext.Items.Where(i => i.type == ItemType.Food || i.type == ItemType.Potion).Include(i => i.consumableStats).ToList();
    }

    public BaseItem? GetById(int id)
    {
        return dbContext.Items.Include(i => i.consumableStats).SingleOrDefault(i => i.id == id);
    }

    public async Task<BaseItem?> Create(Consumable data)
    {
        using var transaction = await dbContext.Database.BeginTransactionAsync();


        // Create item
        dbContext.Items.Add(data);
        await dbContext.SaveChangesAsync();

        var item = dbContext.Items.Find(data.id);

        // Add stats to created item
        foreach (ConsumableStat stat in data.consumableStats)
        {
            item!.consumableStats!.Add(stat.withId(data.id));
        }

        await dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

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

    public async Task<BaseItem?> Update(Consumable data)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        var item = GetById(data.id);

        if (item == null)
        {
            return null;
        }

        // Update base item

        if (data.name != null && data.name != item.name) item.name = data.name;
        if (data.price != null && data.price != item.price) item.price = data.price;
        if (data.type != null && data.type != item.type) item.type = data.type;
        if (data.description != null && data.description != item.description) item.description = data.description;
        if (data.additionalInfo != null && data.additionalInfo != item.additionalInfo) item.additionalInfo = data.additionalInfo;

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
