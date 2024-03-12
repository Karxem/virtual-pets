using virtual_pet.Core.GameEngine.Render;
using virtual_pet.Core.Entities.Items;
using virtual_pet.Core.Level.Common;
using virtual_pet.Core.Models;

namespace virtual_pet.Core.Level
{
    internal class TestLevel : LevelBase
    {
        private static readonly ShopManager shopManager = new ShopManager();
        private Shop shop;

        public override string Name => "Test Level";
        public override bool HasCity => false;
        public override bool HasShop => true;

        public override void StartFight()
        {
            Renderer.MainBuffer.WriteLine("Fight!");
        }

        public override void Display(GameEngine.Render.Buffer buffer)
        {
            buffer.WriteLine("Shopping time!");
            base.Display(buffer);
        }

        public override void VisitShop()
        {
            Renderer.PlayBuffer.WriteLine("Shopping time!");

            List<ItemBase> shopItems = new List<ItemBase>();
            shopItems.Add(new HealingPotion(10) { Count = 1 });
            shopItems.Add(new HealingPotion(50) { Count = 1 });
            shopItems.Add(new HealingPotion(80) { Count = 1 });
            shop = new ShopManager().CreateShop(this, shopItems);

            OpenShop(shop);
        }
    }
}
