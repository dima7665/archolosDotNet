using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using archolosDotNet.Models.Item.Enums;
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

    public ConsumableStat withId(int id)
    {
        this.consumableId = id;
        return this;
    }
}
