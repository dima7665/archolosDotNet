using archolosDotNet.Models;

namespace archolosDotNet.Services;

public static class ItemService
{
    public static void updateItem(BaseItem item, BaseItem data)
    {
        if (data.name != null && data.name != item.name) item.name = data.name;
        if (data.price != null && data.price != item.price) item.price = data.price;
        if (data.type != null && data.type != item.type) item.type = data.type;
        if (data.description != null && data.description != item.description) item.description = data.description;
        if (data.additionalInfo != null && data.additionalInfo != item.additionalInfo) item.additionalInfo = data.additionalInfo;
    }
}
