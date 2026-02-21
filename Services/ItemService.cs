using System.Text.Json;
using archolosDotNet.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace archolosDotNet.Services;

public static class ItemService
{
    static List<Item> Items { get; }

    static ItemService()
    {
        Items = [
            new Item { Id = 1, Name = "Item 1" },
            new Item { Id = 2, Name = "Item 2" },
        ];
    }

    public static List<Item> GetAll() => Items;

    public static Item? Get(int id) => Items.FirstOrDefault(p => p.Id == id);

    public static void Create(Item data)
    {
        int last = Items.Last().Id;
        data.Id = last + 1;
        Items.Add(data);
    }

    public static Item? Update(Item data)
    {
        var index = Items.FindIndex(i => i.Id == data.Id);

        if (index == -1)
        {
            return null;
        }

        Items[index] = data;

        return Items[index];
    }

    public static void Delete(int id)
    {
        var item = Get(id);

        if (item == null)
        {
            return;
        }

        Items.Remove(item);
    }
}
