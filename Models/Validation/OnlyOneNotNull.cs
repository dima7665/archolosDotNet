using System.ComponentModel.DataAnnotations;

namespace archolosDotNet.Models.Validation;

/*
Could be used to check if only one of properties have value

[OnlyOneNotNull("propA", "propB")]
class SomeClass { ... }
*/

[AttributeUsage(AttributeTargets.Class)]
public class OnlyOneNotNullAttribute : ValidationAttribute
{
    private readonly string[] _properties;

    // 'params' allows to pass any number of strings
    public OnlyOneNotNullAttribute(params string[] properties)
    {
        _properties = properties;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        int count = 0;
        var objType = validationContext.ObjectInstance.GetType();

        foreach (var name in _properties)
        {
            var property = objType.GetProperty(name);

            if (property == null) continue;

            var val = property.GetValue(validationContext.ObjectInstance);

            // Check if value is not null (and not an empty string)
            if (val != null && !(val is string s && string.IsNullOrWhiteSpace(s)))
            {
                count++;
            }
        }

        if (count == 1) return ValidationResult.Success;

        return new ValidationResult(ErrorMessage ?? $"Exactly one of these must be set: {string.Join(", ", _properties)}");
    }
}
