using virtual_pet.Core.Entities.Common.Level;
using virtual_pet.Core.Entities.Items;
using virtual_pet.Core.Manager;
using virtual_pet.Core.Models;

namespace virtual_pet.Core.Entities.Levels
{
    internal class TestLevel : LevelBase
    {
        private static readonly ShopManager shopManager = new ShopManager();
        private Shop shop;

        public override string Name => "Test Level";
        public override BiomeType Biome => BiomeType.ForestPath;
        public override bool HasCity => false;
        public override bool HasShop => true;

        public override void StartFight()
        {
            Console.WriteLine("Fight!");
        }

        public override void VisitShop()
        {
            Console.WriteLine("Shopping time!");

            List<ItemBase> shopItems = new List<ItemBase>();
            shopItems.Add(new HealingPotion(1));
            shop = new ShopManager().CreateShop(shopItems);

            shop.DisplayAvailableItems();
        }
    }
}
