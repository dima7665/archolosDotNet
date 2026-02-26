using System.ComponentModel.DataAnnotations;
using archolosDotNet.Models.Item.Enums;
namespace archolosDotNet.Models;

public class BaseItem
{
    [Key]
    public int id { get; set; }

    public required string name { get; set; }

    public required ItemType type { get; set; } = ItemType.Misc;

    public int price { get; set; }

    public string? description { get; set; }

    public string? additionalInfo { get; set; }

    public string[]? sources { get; set; }
}
