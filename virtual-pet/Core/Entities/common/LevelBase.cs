using virtual_pet.Core.Manager;
using virtual_pet.Core.Managers;

namespace virtual_pet.Core.Entities.Common
{
    internal abstract class LevelBase
    {
        private static readonly PetManager petManager = new PetManager();
        private static readonly CombatManager combatManager = new CombatManager();
        private List<PetBase> pets;

        public string Name { get; protected set; }
        public string Theme { get; protected set; }
        public List<string> Messages { get; protected set; }

        public LevelBase()
        {
            pets = petManager.GetPets();
        }

        public void StartFight()
        {
            if (pets == null)
            {
                return;
            }

            combatManager.HandleFight(pets);
        }
    }
}
