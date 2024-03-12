using virtual_pet.Core.Entities.Common.Level;
using virtual_pet.Core.Entities.Items;
using virtual_pet.Core.Manager;
using virtual_pet.Core.Models;

internal class ShopManager
{
    private static readonly InventoryManager inventoryManager = new InventoryManager();

    public Shop CreateShop(LevelBase owner, List<ItemBase> items)
    {
        return new Shop(owner, items);
    }

    public void Purchase(ItemBase item)
    {
        if(item == null) {
            return;
        }

        inventoryManager.SaveItem(item);
    }
}
