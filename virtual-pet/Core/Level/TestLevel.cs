using virtual_pet.Core.Level.Common;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Model;
using virtual_pet.Core.Entity.Items;
using virtual_pet.Core.GameEngine.Render;
using virtual_pet.Core.Manager;

namespace virtual_pet.Core.Level
{
    internal class TestLevel : LevelBase
    {
        private static readonly PetManager petManager = new PetManager();
        private static readonly CombatManager combatManager = new CombatManager();
        private Shop shop;

        public override string Name => "Test Level";
        public override bool HasCity => false;
        public override bool HasShop => true;

        public override void Display(GameEngine.Render.Buffer buffer)
        {
            buffer.WriteLine("Shopping time!");
            base.Display(buffer);
        }

        public override void StartFight()
        {
            combatManager.HandleFight(petManager.GetPets());
        }

        public override void VisitShop()
        {
            Renderer.PlayBuffer.WriteLine("Shopping time!");

            List<ItemBase> shopItems = new List<ItemBase>();
            shopItems.Add(new HealingPotion(10) { Count = 1, Description = "Heals 10 HP" });
            shopItems.Add(new HealingPotion(50) { Count = 2, Description = "Heals 50 HP" });
            shopItems.Add(new HealingPotion(80) { Count = 3, Description = "Heals 80 HP" });
            shop = new Shop(this, shopItems).CreateShop();

            OpenShop(shop);
        }
    }
}
