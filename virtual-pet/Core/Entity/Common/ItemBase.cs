using System;
using virtual_pet.Core.Entities.Common;

namespace virtual_pet.Core.Entities.Items
{
    public abstract class ItemBase
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public int Count { get; set; }

        public void AddItem(int amount) => Count += amount;
        public void RemoveItem(int amount) => Count -= amount;

        public abstract void UseItem(PetBase targetPet);
        public abstract void DisplayItemInfo();
        public abstract void ApplyEffect(PetBase targetPet);
    }
}
