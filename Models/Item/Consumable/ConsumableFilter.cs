using System;
using archolosDotNet.Models.Item.Enums;

namespace archolosDotNet.Models.Item.ConsumableNS;

public class ConsumableFilter
{
    public ConsumableType? type { get; set; }

    public ConsumableAttr? stat { get; set; }

    public bool? isPermanent { get; set; }
}
