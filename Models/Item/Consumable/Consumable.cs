using System.ComponentModel.DataAnnotations;
using archolosDotNet.Models.Item.Enums;
using archolosDotNet.Models.Item.RecipeNS;

namespace archolosDotNet.Models.Item.ConsumableNS;

public interface IConsumable
{
    public ICollection<ConsumableStat> consumableStats { get; set; }
}

public class Consumable : BaseItem, IConsumable
{
    public required ConsumableType type { get; set; }
    public required ICollection<ConsumableStat> consumableStats { get; set; }

    public RecipeIngredient? asIngredient { get; set; } // navigation for Foreign keys
    public ICollection<Recipe> recipes { get; set; } = []; // navigation for Foreign keys
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
