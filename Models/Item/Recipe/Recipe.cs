using System.ComponentModel.DataAnnotations;
using archolosDotNet.Models.Item.Enums;
using archolosDotNet.Models.Item.ConsumableNS;
using archolosDotNet.Models.Item.WeaponNS;
using archolosDotNet.Models.Item.Miscellaneous;
using archolosDotNet.Models.Item.ArmorNS;
using archolosDotNet.Models.SelectNS;

namespace archolosDotNet.Models.Item.RecipeNS;

public class Recipe : BaseItem
{
    public required ICollection<RecipeIngredient> ingredients { get; set; } = [];

    public required RecipeSkill requirement { get; set; }
    public int? requirementLevel { get; set; }

    public int? consumableId { get; set; } // target
    public Consumable? consumable { get; set; }

    public int? weaponId { get; set; } // target
    public Weapon? weapon { get; set; }

    public int? miscId { get; set; } // target
    public Misc? misc { get; set; }

    public int? armorId { get; set; } // target
    public Armor? armor { get; set; }
}

public class RecipeIngredient
{
    [Key]
    public int id { get; set; }

    public int quantity { get; set; }

    public int recipeId { get; set; } // Foreign key

    public int? consumableId { get; set; }
    public Consumable? consumable { get; set; }

    public int? weaponId { get; set; }
    public Weapon? weapon { get; set; }

    public int? miscId { get; set; }
    public Misc? misc { get; set; }

    public int? armorId { get; set; }
    public Armor? armor { get; set; }
}

public class RecipeIngredientWithName : RecipeIngredient
{
    public string? name { get; set; }

}

public class RecipeShort : BaseItem
{
    public ICollection<RecipeIngredientShort> ingredients { get; set; } = [];

    public RecipeSkill requirement { get; set; }
    public int? requirementLevel { get; set; }
}

public class RecipeShortWithTarget : RecipeShort
{
    public Consumable? consumable { get; set; }

    public Weapon? weapon { get; set; }

    public Misc? misc { get; set; }

    public Armor? armor { get; set; }
}

public class RecipeIngredientShort
{
    public int id { get; set; }

    public required string name { get; set; }

    public int quantity { get; set; }

    public int? consumableId { get; set; }
    public int? weaponId { get; set; }
    public int? miscId { get; set; }
}

public class IngredientsList
{
    public List<SelectOption> misc { get; set; } = [];
    public List<SelectOption> consumables { get; set; } = [];
    public List<SelectOption> weapons { get; set; } = [];
}