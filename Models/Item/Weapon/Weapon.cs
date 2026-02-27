using archolosDotNet.Models.Item.Enums;

namespace archolosDotNet.Models.Item.Weapon;

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
}
