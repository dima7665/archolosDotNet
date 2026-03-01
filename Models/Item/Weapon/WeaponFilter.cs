using archolosDotNet.Models.Item.Enums;

namespace archolosDotNet.Models.Item.WeaponNS;

public class WeaponFilter
{
    public WeaponType? type { get; set; }
    public WeaponDamageType? damageType { get; set; }
    public WeaponSkill? skill { get; set; }
    public int? skillRequirement { get; set; }
    public int? skillBonus { get; set; }
}
