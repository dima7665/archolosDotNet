using archolosDotNet.Models.Item.Enums;

namespace archolosDotNet.Models.Item.RecipeNS;

public class RecipeFilter
{
    public RecipeSkill? skill { get; set; }
    public RecipeItemType? itemType { get; set; }
}
