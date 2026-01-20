using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem item)
        {
            return new Dtos.InventoryItemDto(item.CatalogItemId, item.Quantity, item.AcquiredDate);
        }
    }
}