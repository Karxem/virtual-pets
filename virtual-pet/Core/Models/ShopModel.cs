using virtual_pet.Core.Entities.Items;
using virtual_pet.Core.Render;

namespace virtual_pet.Core.Models
{
    internal class Shop
    {
        private List<ItemBase> availableItems;

        public Shop(List<ItemBase> items)
        {
            availableItems = items;
        }

        public void DisplayAvailableItems()
        {
            Renderer.MainBuffer.WriteLine("Welcome to the shop. What would you like to do?");
            foreach (ItemBase item in availableItems)
            {
                Renderer.MainBuffer.WriteLine(item.Name);
            }
        }

        public void SellItem(ItemBase item)
        {
            // Logic to sell an item to the player's pet
        }
    }
}
