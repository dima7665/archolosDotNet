using System;
using archolosDotNet.Models.Item.Enums;

namespace archolosDotNet.Models.Item.Consumable;

public class ConsumableFilter
{
    public ConsumableType? type { get; set; }

    public ConsumableAttr? stat { get; set; }

    public bool? isPermanent { get; set; }
}
