using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication;

namespace archolosDotNet.Models.Item.Consumable;

public interface IConsumable
{
    public ICollection<ConsumableStat> consumableStats { get; set; }
}

public class Consumable : BaseItem, IConsumable
{
    public new required ICollection<ConsumableStat> consumableStats { get; set; }
}

public class ConsumableStat
{
    public required ConsumableAttr stat { get; set; }
    public bool isPermanent { get; set; }
    public bool isPercentage { get; set; }
    public int? duration { get; set; }
    public int? value { get; set; }

    [Key]
    public int id { get; set; }

    public int consumableId { get; set; } // Foreign key
}

public interface IConsumableDto
{
    public ICollection<ConsumableStatDto> consumableStats { get; set; }
}

public class ConsumableDto : BaseItem, IConsumableDto
{
    public new required ICollection<ConsumableStatDto> consumableStats { get; set; }
}

public class ConsumableStatDto
{
    public required ConsumableAttr stat { get; set; }
    public bool isPermanent { get; set; }
    public bool isPercentage { get; set; }
    public int? duration { get; set; }
    public int? value { get; set; }

    public ConsumableStat toStat(int _id)
    {
        return new ConsumableStat
        {
            stat = stat,
            isPermanent = isPermanent,
            isPercentage = isPercentage,
            duration = duration,
            value = value,
            consumableId = _id
        };
    }
}

public enum ConsumableAttr
{
    Health,
    Mana,
    Strength,
    Dexterity,
    Armor,
    Spellpower,
    Exp,
    Speed,
    Underwater,
}
