using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Manager;

namespace virtual_pet.Core.Entity.Items
{
    internal class HealingPotion : ItemBase
    {
        private static readonly PetManager petManager = new PetManager();
        public int HealingAmount { get; private set; }

        public override string Name => "Healing Potion";

        public override string Description => "Heals half of the HP points of a pet.";

        public HealingPotion(int healingAmount)
        {
            HealingAmount = healingAmount;
        }

        public override void UseItem(PetBase targetPet)
        {
            ApplyEffect(targetPet);
            RemoveItem(1);

            Console.Clear();
            Console.WriteLine($"{targetPet.Name} used {Name} and healed for {HealingAmount} health points.\n");
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
            petManager.SavePet(targetPet);
        }
    }
}
