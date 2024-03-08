using virtual_pet.Core.Entities.Common;

namespace virtual_pet.Core.Entities.Pets
{
    internal class Flololo : PetBase
    {
        public Flololo()
        {
            RegisterPetType(GetPetType());
        }

        public override string GetPetType()
        {
            return "Flololo";
        }
    }
}
