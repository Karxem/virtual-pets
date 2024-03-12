using virtual_pet.Core.Manager;
using virtual_pet.Core.Managers;
using virtual_pet.Core.Render;

namespace virtual_pet.Core.Entities.Common.Level
{
    internal abstract class LevelBase
    {
        private static readonly PetManager petManager = new PetManager();
        private static readonly CombatManager combatManager = new CombatManager();
        private List<PetBase> pets;

        public abstract string Name { get;}
        public abstract BiomeType Biome { get;}
        public abstract bool HasCity { get; }
        public abstract bool HasShop { get; }

        public LevelBase()
        {
            pets = petManager.GetPets();
        }

        public void StartLevel()
        {
            LevelEvent e = EventGenerator.GenerateRandomEvent();
            Renderer.PlayBuffer.WriteLine(e.ToString());
            HandleEvent(e);
        }
        private void HandleEvent(LevelEvent e)
        {
            switch (e)
            {
                case LevelEvent.WildPetAttack:
                    StartFight();
                    break;
                case LevelEvent.ShopVisit:
                    VisitShop();
                    break;
                default:
                    Renderer.PlayBuffer.WriteLine("Invalid LevelEvent was passed.");
                    break;
            }
        }

        public abstract void StartFight();
        public abstract void VisitShop();
    }
}
