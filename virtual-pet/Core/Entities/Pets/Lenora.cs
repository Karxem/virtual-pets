using virtual_pet.Core.Entities.Common;

namespace virtual_pet.Core.Entities.Pets
{
    internal class Lenora : PetBase
    {
        public Lenora()
        {
            RegisterPetType(GetPetType());
        }

        public override string GetPetType()
        {
            return "Lenora";
        }
    }
}
