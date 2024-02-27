using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Managers;
using virtual_pet.Core.Models;
using virtual_pet.Core.Utils;
using virtual_pet.Core.Render;
using virtual_pet.Core.Input;

namespace virtual_pet.Application
{
    public class VirtualPets
    {
        static PlayerController controller = new PlayerController();
        static List<string> menuItems = new List<string>
            {
            "Show Pet Overview",
            "Fill all pet stats",
            "Option 3",
            "Exit"
            };
        static ConsoleMenuBuffered consoleMenu = new ConsoleMenuBuffered(Renderer.MenuBuffer, onItemSelected, menuItems);
        static PetManager petManager = new PetManager();
        static List<Pet> pets = new List<Pet>();
        static bool running = true;

        static void Main()
        {
            Console.CursorVisible = false;
            //int selectedIndex = consoleMenu.ShowMenu();
            pets = petManager.GetPets();
            controller.Listener = consoleMenu;

            while (running)
            {
                //clear buffers
                Renderer.ClearBuffers();
                //fetch input
                controller.Tick();
                //
                consoleMenu.Display();
                //render the scene
                Renderer.Render();
                Thread.Sleep(30);
            }

            Console.WriteLine("Exiting...");

        }


        public static void onItemSelected(object sender, int selectedIndex) {
            switch (selectedIndex)
            {
                case 0:
                    Renderer.MainBuffer.WriteLine("Your Pets:");
                    Renderer.MainBuffer.WriteLine("-------------------------------------------------------");
                    foreach (var pet in pets)
                    {
                        Renderer.MainBuffer.WriteLine($"Name: {pet.Name}, Energy: {pet.Energy}, Hunger: {pet.Hunger}, Thirst: {pet.Thirst}");
                    }
                    break;
                case 1:
                    foreach (var pet in pets)
                    {
                        var petInstance = petManager.LoadPet(pet.Name);
                        petInstance.Eat(100);
                        petInstance.Drink(100);
                        petInstance.Sleep(100);

                        petManager.SavePet(petInstance);
                        Renderer.MainBuffer.WriteLine($"{pet.Name} is filled up again!");
                    }
                    break;
                case 2:
                    Renderer.MainBuffer.WriteLine("Option 3");
                    break;
                case 3:
                    running = false;
                    break;
                default:
                    Renderer.MainBuffer.WriteLine("An error occured");
                    break;
            }
        }
    }
}