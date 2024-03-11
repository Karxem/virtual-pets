using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Managers;
using virtual_pet.Core.Models;
using virtual_pet.Core.Utils;
using virtual_pet.Core.Render;
using virtual_pet.Core.Input;
using virtual_pet.Core;

namespace virtual_pet.Application {
    public class VirtualPets {
        
        static List<string> menuItems = new List<string>
            {
            "Show Pet Overview",
            "Fill all pet stats",
            "Add a pet",
            "Exit",
            };
       // static ConsoleMenu consoleMenu = new ConsoleMenu( onItemSelected, menuItems);


        static PetManager petManager = new PetManager();
        static List<PetModel> pets = new List<PetModel>();
        
        static MainInputListener mainListener = new MainInputListener();

        static bool running = true;




        static void Main() {

            //inputListeners.Push(new MainInputListener());
            Console.CursorVisible = false;
            //debugMenu.CreateClickItem("Debug Color Print");
            //debugTextInput = debugMenu.CreateTextInputItem(onDebugTextInput, "Debug Text Input Item").Input;

            Engine.Init();
            Renderer.Init();
            //int selectedIndex = consoleMenu.ShowMenu();
            pets = petManager.GetPets();
            //PassInput(mainListener);
            running = true;
            while (running)
            {
                Renderer.ClearBuffers();
                Engine.Tick();
                Renderer.Render();
                Thread.Sleep(16);
            }

            Console.WriteLine("Exiting...");

        }


        public static void onItemSelected(object sender, int selectedIndex) {
            switch (selectedIndex)
            {
                case 0:
                    //Console.Clear();
                    Renderer.PlayBuffer.WriteLine("Your Pets:");
                    Renderer.PlayBuffer.WriteLine("-------------------------------------------------------");

                    foreach (var pet in pets)
                    {
                        Renderer.PlayBuffer.WriteLine($"Name: {pet.Name}, Health: {pet.Health}, Energy: {pet.Energy}, Hunger: {pet.Hunger}, Thirst: {pet.Thirst}");
                    }
                    break;
                case 1:
                    //Console.Clear();

                    foreach (var pet in pets)
                    {
                        var petInstance = petManager.LoadPet(pet.Name);
                        petInstance.FillAll();
                        petManager.SavePet(petInstance);

                        Renderer.PlayBuffer.WriteLine($"{pet.Name} is filled up again!");
                    }
                    break;
                case 2:
                    //Console.Clear();

                    Console.Write("Name your new Pet: ");
                    string petName = Console.ReadLine();

                    PetBase newPet = new Lenora();
                    newPet.PetName = petName;
                    newPet.FillAll();
                    petManager.SavePet(newPet);

                    Console.WriteLine($"Your new pet {petName} was added");
                    break;
                case 3:
                    running = false;
                    return;
                default:
                    Console.WriteLine("An error occured");
                    break;
            }

            Renderer.MainBuffer.WriteLine("\nPress Enter to go back to the main menu...");
            Console.ReadLine();
        }        
    }
}
