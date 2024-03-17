using virtual_pet.Core.Entity.Common;

namespace virtual_pet.Core.Entity.Pets
{
    internal class Lenora : PetBase
    {

        public override string GetPetType()
        {
            return "Lenora";
        }

        public override void UseAbility()
        {
            Console.WriteLine(Name + " used its ability!");
        }

        public override string GetPetSprite()
        {
            string sprite =
@"  .-""""""-.
 /      o\
|    o   0).-.
|       .-;(_/     .-.
 \     /  /)).---._|  `\   ,
  '.  '  /((       `'-./ _/|
    \  .'  )        .-.;`  /
     '.             |  `\-'
       '._        -'    /
         ``--""""""`------`";

            return sprite;
        }
    }
}
