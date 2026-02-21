using System;

namespace archolosDotNet.Models;

public class Item
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public int Price { get; set; }
    public decimal? Temp { get; set; }
}
