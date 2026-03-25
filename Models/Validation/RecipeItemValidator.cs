using System.ComponentModel.DataAnnotations;
using archolosDotNet.Models.Item.RecipeNS;

namespace archolosDotNet.Models.Validation;

// Use to validate recipe product or ingredient - should contain only one type of id (misc, weapon or consumable)
[AttributeUsage(AttributeTargets.Class)]
public class RecipeItemRelationsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object _value, ValidationContext validationContext)
    {
        IRecipeItemRelations value = (IRecipeItemRelations)_value;
        int count = 0;

        if (value.consumableId != null) count++;
        if (value.weaponId != null) count++;
        if (value.miscId != null) count++;

        if (count == 1) return ValidationResult.Success;

        return new ValidationResult(ErrorMessage ?? $"Exactly one type of item should be provided");
    }
}

// Use to validate recipe - product and ingredients should not have duplicates
[AttributeUsage(AttributeTargets.Class)]
public class RecipeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        Recipe recipe = (Recipe)value;

        // check same product and ingredient
        bool productIngredientError = false;

        // check duplicate ingredient
        var seenMiscIds = new HashSet<int?>();
        var seenWeaponIds = new HashSet<int?>();
        var seenConsumableIds = new HashSet<int?>();
        var duplicateMisc = false;
        var duplicateWeapon = false;
        var duplicateConsumable = false;

        var ingredients = recipe.ingredients;

        foreach (var ing in ingredients)
        {
            // check duplicate ingredient
            if (ing.miscId.HasValue) duplicateMisc = !seenMiscIds.Add(ing.miscId);
            if (ing.weaponId.HasValue) duplicateWeapon = !seenWeaponIds.Add(ing.weaponId);
            if (ing.consumableId.HasValue) duplicateConsumable = !seenConsumableIds.Add(ing.consumableId);

            // check same product and ingredient
            if (ing.miscId != null && ing.miscId == recipe.miscId) productIngredientError = true;
            if (ing.weaponId != null && ing.weaponId == recipe.weaponId) productIngredientError = true;
            if (ing.consumableId != null && ing.consumableId == recipe.consumableId) productIngredientError = true;
        }

        if (productIngredientError) return new ValidationResult("Product of recipe should not be used as ingredient in same recipe");

        if (duplicateMisc || duplicateWeapon || duplicateConsumable) return new ValidationResult("Ingredients should not contain duplicates");

        return ValidationResult.Success;
    }
}
