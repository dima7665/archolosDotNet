using archolosDotNet.EF;
using archolosDotNet.Models.Item.Miscellaneous;

namespace archolosDotNet.Services.Item;

public interface IMiscService
{
    public IQueryable<Misc> GetAll(MiscFilter? filters);
    public Misc? GetById(int id);
    public Misc? Create(Misc data);
    public Misc? Delete(int id);
    public Misc? Update(Misc data);
}

public class MiscService(ApplicationDbContext context) : IMiscService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<Misc> GetAll(MiscFilter? filter)
    {
        var list = dbContext.Miscs.AsQueryable();

        if (filter != null && filter.name?.Length > 1)
        {
            list = list.Where(i => i.name == filter.name);
        }

        return list;
    }

    public Misc? GetById(int id)
    {
        return dbContext.Miscs.SingleOrDefault(i => i.id == id);
    }

    public Misc? Create(Misc data)
    {
        dbContext.Miscs.Add(data);
        dbContext.SaveChanges();

        return data;
    }

    public Misc? Delete(int id)
    {
        var item = dbContext.Miscs.Find(id);

        if (item == null)
        {
            return null;
        }

        dbContext.Miscs.Remove(item);
        dbContext.SaveChanges();

        return item;
    }

    public Misc? Update(Misc data)
    {

        var item = GetById(data.id);

        if (item == null)
        {
            return null;
        }

        ItemService.updateItem(item, data);

        dbContext.SaveChanges();

        return item;
    }
}
