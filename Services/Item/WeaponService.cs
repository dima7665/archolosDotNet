using archolosDotNet.Database;
using archolosDotNet.Models.Item.WeaponNS;

namespace archolosDotNet.Services.Item;

public interface IWeaponService
{
    public IQueryable<Weapon> GetAll(WeaponFilter? filters);
    public Weapon? GetById(int id);
    public Weapon? Create(Weapon data);
    public Weapon? Delete(int id);
    public Weapon? Update(Weapon data);
}

public class WeaponService(ApplicationDbContext context) : IWeaponService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<Weapon> GetAll(WeaponFilter? filter)
    {
        var list = dbContext.Weapons.AsQueryable();

        if (filter != null && filter.type.HasValue)
        {
            list = list.Where(i => i.type == filter.type);
        }

        if (filter != null && filter.damageType.HasValue)
        {
            list = list.Where(i => i.damageType == filter.damageType);
        }

        return list;
    }

    public Weapon? GetById(int id)
    {
        return dbContext.Weapons.SingleOrDefault(i => i.id == id);
    }

    public Weapon? Create(Weapon data)
    {
        dbContext.Weapons.Add(data);
        dbContext.SaveChanges();

        return data;
    }

    public Weapon? Delete(int id)
    {
        var item = dbContext.Weapons.Find(id);

        if (item == null)
        {
            return null;
        }

        dbContext.Weapons.Remove(item);
        dbContext.SaveChanges();

        return item;
    }

    public Weapon? Update(Weapon data)
    {

        var item = GetById(data.id);

        if (item == null)
        {
            return null;
        }

        ItemService.updateItem(item, data);

#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
        if (data.type != null && data.type != item.type) item.type = data.type;
        if (data.damage != null && data.damage != item.damage) item.damage = data.damage;
        if (data.damageType != null && data.damageType != item.damageType) item.damageType = data.damageType;
        if (data.range != null && data.range != item.range) item.range = data.range;
        if (data.armorPiercing != null && data.armorPiercing != item.armorPiercing) item.armorPiercing = data.armorPiercing;
        if (data.skill != null && data.skill != item.skill) item.skill = data.skill;
        if (data.skillRequirement != null && data.skillRequirement != item.skillRequirement) item.skillRequirement = data.skillRequirement;
        if (data.skillBonus != null && data.skillBonus != item.skillBonus) item.skillBonus = data.skillBonus;
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'

        dbContext.SaveChanges();

        return item;
    }
}
