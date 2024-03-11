using virtual_pet.Core.Entities.Items;

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
            Console.WriteLine("Welcome to the shop. What would you like to do?");
            foreach (ItemBase item in availableItems)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void SellItem(ItemBase item)
        {
            // Logic to sell an item to the player's pet
        }
    }
}
