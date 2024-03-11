﻿using virtual_pet.Core.Entities.Common;

namespace virtual_pet.Core.Entities.Pets
{
    internal class Flololo : PetBase
    {
        public override string GetPetType()
        {
            return "Flololo";
        }
        public override void UseAbility()
        {
            Console.WriteLine(this.Name + " used its ability!");
        }

    }
}
