using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Manager;
using virtual_pet.Core.Util;

namespace virtual_pet.Core.Entity.Items
{
    internal class HealingPotion : ItemBase
    {
        private static readonly PetManager petManager = new PetManager();

        public override string Name => "Healing Potion";

        public int HealingAmount { get; private set; }

        public HealingPotion(int healingAmount)
        {
            Description = "A potion that heals your pet for a certain amount of health points.";
            HealingAmount = healingAmount;
        }

        public override void UseItem(PetBase targetPet)
        {
            ApplyEffect(targetPet);
            RemoveItem(1);

            Renderer.SendNotification($"{targetPet.Name} used {Name} and healed for {HealingAmount} health points.");
        }

        public override void DisplayItemInfo()
        {
            Console.WriteLine($"Item: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Healing Amount: {HealingAmount}");
        }

        public override void ApplyEffect(PetBase targetPet)
        {
            targetPet.Heal(HealingAmount);
        }
    }
}
