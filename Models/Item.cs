using System;
using System.ComponentModel.DataAnnotations;

namespace archolosDotNet.Models;

public class BaseItem
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }
    public int Price { get; set; }
    public decimal? Temp { get; set; }
}

public class Item: BaseItem {}