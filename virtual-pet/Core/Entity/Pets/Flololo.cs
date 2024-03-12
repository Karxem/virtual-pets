using virtual_pet.Core.Entity.Common;

namespace virtual_pet.Core.Entity.Pets
{
    internal class Flololo : PetBase
    {
        public override string GetPetType()
        {
            return "Flololo";
        }
        public override void UseAbility()
        {
            Console.WriteLine(Name + " used its ability!");
        }

    }
}
