// using archolosDotNet.Models;

// namespace archolosDotNet.Services;

// public static class ItemService
// {
//     static List<BaseItem> Items { get; }

//     static ItemService()
//     {
//         Items = [
//             new BaseItem { id = 1, name = "Item 1", type = ItemType.Food },
//             new BaseItem { id = 2, name = "Item 2", type = ItemType.Weapon },
//         ];
//     }

//     public static List<BaseItem> GetAll() => Items;

//     public static BaseItem? Get(int id) => Items.FirstOrDefault(p => p.id == id);

//     public static void Create(BaseItem data)
//     {
//         int last = Items.Last().id;
//         data.id = last + 1;
//         Items.Add(data);
//     }

//     public static BaseItem? Update(BaseItem data)
//     {
//         var index = Items.FindIndex(i => i.id == data.id);

//         if (index == -1)
//         {
//             return null;
//         }

//         Items[index] = data;

//         return Items[index];
//     }

//     public static void Delete(int id)
//     {
//         var item = Get(id);

//         if (item == null)
//         {
//             return;
//         }

//         Items.Remove(item);
//     }
// }
