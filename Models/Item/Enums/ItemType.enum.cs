
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace archolosDotNet.Models.Item.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ItemType
{
    [EnumMember(Value = "food")]
    Food,

    [EnumMember(Value = "potion")]
    Potion,

    [EnumMember(Value = "weapon")]
    Weapon,

    [EnumMember(Value = "armor")]
    Armor,

    [EnumMember(Value = "jewelry")]
    Jewelry,

    [EnumMember(Value = "misc")]
    Misc,

    [EnumMember(Value = "recipe")]
    Recipe
}
