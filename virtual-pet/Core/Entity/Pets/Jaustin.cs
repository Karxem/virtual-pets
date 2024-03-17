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

        public override string GetPetSprite()
        {
            string sprite =
@"            _..-'(                       )`-.._
           ./'. '||\\.       (\_/)       .//||` .`\.
        ./'.|'.'||||\\|..    )O O(    ..|//||||`.`|.`\.
     ./'..|'.|| |||||\`````` '`""'` ''''''/||||| ||.`|..`\.
   ./'.||'.|||| ||||||||||||.     .|||||||||||| |||||.`||.`\.
  /'|||'.|||||| ||||||||||||{     }|||||||||||| ||||||.`|||`\
 '.|||'.||||||| ||||||||||||{     }|||||||||||| |||||||.`|||.`
'.||| ||||||||| |/'   ``\||``     ''||/''   `\| ||||||||| |||.`
|/' \./'     `\./         \!|\   /|!/         \./'     `\./ `\|
V    V         V          }' `\ /' `{          V         V    V
`    `         `               V               '         '    '`";

            return sprite;
        }
    }
}
