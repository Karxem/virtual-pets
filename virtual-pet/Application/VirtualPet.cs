using virtual_pet.Core.Manager;

namespace virtual_pet.Application
{
    public class VirtualPets
    {
        static StateManager stateManager = new StateManager();

        static void Main()
        {
            stateManager.Initialize();
        }
    }
}
