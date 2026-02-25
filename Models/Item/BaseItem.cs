using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using archolosDotNet.Models.Item.Consumable;
using archolosDotNet.Models.Item.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace archolosDotNet.Models;

public class _BaseItem
{
    [Key]
    public int id { get; set; }

    public required string name { get; set; }

    public int price { get; set; }

    public string? description { get; set; }

    public string? additionalInfo { get; set; }

    public string[]? sources { get; set; }
}

public class BaseItem : _BaseItem
{
    public required ItemType type { get; set; }

    public ICollection<ConsumableStat>? consumableStats { get; } = [];

    public BaseItem() : base() { }
}


// public class BaseItemDto : _BaseItem
// {
//     public required string type { get; set; }

//     public BaseItemDto() : base() { }

//     [SetsRequiredMembers]
//     public BaseItemDto(BaseItem data)
//     {
//         type = getType(data.type);

//         name = data.name;
//         id = data.id;
//         description = data.description;
//         additionalInfo = data.additionalInfo;
//         sources = data.sources;
//         price = data.price;
//     }

//     private string getType(ItemType _type)
//     {
//         return _type switch
//         {
//             ItemType.Food => "food",
//             ItemType.Potion => "potion",
//             ItemType.Weapon => "weapon",
//             ItemType.Armor => "armor",
//             ItemType.Jewelry => "jewelry",
//             ItemType.Recipe => "recipe",
//             ItemType.Misc => "misc",
//             _ => "misc",
//         };
//     }
// }


/*
можна працювати з стрінгами, але в базу записується теж стрінг
private ItemType _type;
public required string type
{
get
{
    return _type switch
    {
        ItemType.Food => "food",
        _ => "misc",
    };
}
set
{
    // _type = JsonConvert.DeserializeObject<ItemType>(type); // Serialize/Deserialize не працює - перетворює об'єкти а не значення
    _type = type switch
    {
        "food" => ItemType.Food,
        _ => ItemType.Misc,
    };
}
}
*/
