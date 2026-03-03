using archolosDotNet.Models.Item.RecipeNS;

namespace archolosDotNet.Models.Item.Miscellaneous;

public class Misc : BaseItem
{
    public ICollection<RecipeIngredient> asIngredient { get; set; } = []; // navigation for Foreign keys

    public ICollection<Recipe> recipes { get; set; } = []; // navigation for Foreign keys
}
