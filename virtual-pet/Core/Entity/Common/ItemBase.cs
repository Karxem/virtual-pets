using System;

namespace virtual_pet.Core.Entity.Common
{
    public abstract class ItemBase
    {
        public abstract string Name { get;  }
        public string Description { get; set;  }
        public int Count { get; set; }

        public void AddItem(int amount) => Count += amount;
        public void RemoveItem(int amount) => Count -= amount;

        public abstract void UseItem(PetBase targetPet);
        public abstract void DisplayItemInfo();
        public abstract void ApplyEffect(PetBase targetPet);
    }
}
