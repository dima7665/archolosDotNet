using System.ComponentModel.DataAnnotations;
using archolosDotNet.Models.Item.Enums;

namespace archolosDotNet.Models.Item.Recipe;

public interface IRecipe
{
    public ICollection<RecipeIngredient> ingredients { get; set; }

    public int targetId { get; set; }
    public RecipeItemType targetType { get; set; }
    public RecipeSkill requirement { get; set; }
    public int? requirementLevel { get; set; }

}

public class Recipe : BaseItem, IRecipe
{
    public required int targetId { get; set; } // id of item
    public required RecipeItemType targetType { get; set; } // type of item

    public required ICollection<RecipeIngredient> ingredients { get; set; } = [];

    public RecipeSkill requirement { get; set; }
    public int? requirementLevel { get; set; }
}

public class RecipeIngredient
{
    [Key]
    public int id { get; set; }

    public int quantity { get; set; }

    public int recipeId { get; set; } // Foreign key

    public int itemId { get; set; }
    public required RecipeItemType itemType { get; set; } // type of item
}

public class RecipeIngredientWithName: RecipeIngredient
{
    public string? name { get; set; }

}

