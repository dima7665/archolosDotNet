using archolosDotNet.Models.Item.Enums;
using archolosDotNet.Models.Item.RecipeNS;

namespace archolosDotNet.Models.Item.WeaponNS;

public interface IWeapon
{
    public WeaponType type { get; set; }
    public int damage { get; set; }
    public WeaponDamageType damageType { get; set; }
    public int? range { get; set; }
    public int? armorPiercing { get; set; }
    public WeaponSkill skill { get; set; }
    public int? skillRequirement { get; set; }
    public int? skillBonus { get; set; }
}

public class Weapon : BaseItem, IWeapon
{
    public required WeaponType type { get; set; }
    public required int damage { get; set; }
    public required WeaponDamageType damageType { get; set; }
    public int? range { get; set; }
    public int? armorPiercing { get; set; }
    public WeaponSkill skill { get; set; }
    public int? skillRequirement { get; set; }
    public int? skillBonus { get; set; }

    public ICollection<RecipeIngredient> asIngredient { get; set; } = []; // navigation for Foreign keys

    public ICollection<Recipe> recipes { get; set; } = []; // navigation for Foreign keys
}
