using virtual_pet.Core.Entities.Common;

namespace virtual_pet.Core.Entities.Pets
{
    internal class Jaustin : PetBase
    {
        public override string GetPetType()
        {
            return "Jaustin";
        }

        public override void UseAbility()
        {
            Console.WriteLine(this.Name + " used its ability!");
        }
    }
}
