using virtual_pet.Core.Entity.Common;

namespace virtual_pet.Core.Entity.Pets
{
    internal class Jaustin : PetBase
    {
        public override string GetPetType()
        {
            return "Jaustin";
        }

        public override void UseAbility()
        {
            Console.WriteLine(Name + " used its ability!");
        }
    }
}
