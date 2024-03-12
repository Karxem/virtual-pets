using virtual_pet.Core.Entities.Common;

namespace virtual_pet.Core.Entities.Pets
{
    internal class Lenora : PetBase
    {

        public override string GetPetType()
        {
            return "Lenora";
        }

        public override void UseAbility()
        {
            Console.WriteLine(this.Name + " used its ability!");
        }
    }
}
