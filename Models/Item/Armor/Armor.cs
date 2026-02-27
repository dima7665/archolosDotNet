using System.ComponentModel.DataAnnotations;
using archolosDotNet.Models.Item.Enums;

namespace archolosDotNet.Models.Item.Armor;

public interface IArmor
{
    public ICollection<ArmorStatObj> stats { get; set; }
}

public class Armor : BaseItem, IArmor
{
    public required ICollection<ArmorStatObj> stats { get; set; } = [];
}

public class ArmorStatObj
{
    [Key]
    public int id { get; set; }

    public required ArmorStat type { get; set; }
    public required int value { get; set; }
    public int armorId { get; set; } // Foreign key
}
