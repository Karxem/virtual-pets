using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Managers;
using virtual_pet.Core.Models;
using virtual_pet.Core.Utils;
using virtual_pet.Core.Render;
using virtual_pet.Core.Input;
using virtual_pet.Core;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;

namespace virtual_pet.Application {
    public class VirtualPets {
        static PlayerController controller = new PlayerController();
        static List<string> menuItems = new List<string>
            {
            "Show Pet Overview",
            "Fill all pet stats",
            "Add a pet",
            "Exit",
            "5",
            "6",
            "7",
            "8"
            };
        static ConsoleMenu consoleMenu = new ConsoleMenu( onItemSelected, menuItems);

        static List<string> mainMenuItems = new List<string>
            {
            "Change Interface Size",
            "Close Game"
            };
        static ConsoleMenu mainMenu = new ConsoleMenu( onMainItemSelected, mainMenuItems);


        static List<MenuItemBase> debugMenuItems = new List<MenuItemBase>
            {
            
            };
        static ConsoleMenu debugMenu = new ConsoleMenu( onDebugItemSelected, debugMenuItems);

        static TextInput debugTextInput;//= TextInput.GetTextNumberInput(onDebugTextInput);

        static Stack<ConsoleMenu> menus = new Stack<ConsoleMenu>();
        static Stack<IInputListener> inputListeners = new Stack<IInputListener>();

        static ConsoleMenu currentMenu ;
        static OptionStrip? currentOtionStrip;

        static PetManager petManager = new PetManager();
        static List<PetModel> pets = new List<PetModel>();
        static List<IDisplayable> displayables = new List<IDisplayable>();
        static DebugPrint debugPrint = new DebugPrint();
        static MainInputListener mainListener = new MainInputListener();

        static bool running = true;

        public static void PassInput(IInputListener listener) {
            if(controller.Listener != null)
                controller.Listener.IsActive = false;
            inputListeners.Push(controller.Listener);
            controller.Listener = listener;
            listener.IsActive = true;
            currentOtionStrip = listener.GetOptionStrip();
        }

        public static void PopInput() {
            controller.Listener.IsActive = false;
            controller.Listener = inputListeners.Pop();
            controller.Listener.IsActive = true;
            currentOtionStrip = controller.Listener?.GetOptionStrip();
        }

        public static void OpenMenu(ConsoleMenu consoleMenu) {
            menus.Push(currentMenu);
            currentMenu = consoleMenu;
            PassInput(consoleMenu);
        }

        public static void CloseMenu() {
            currentMenu = menus.Pop();
            PopInput();
        }


        static void Main() {

            inputListeners.Push(new MainInputListener());
            Console.CursorVisible = false;
            debugMenu.CreateClickItem("Debug Color Print");
            debugTextInput = debugMenu.CreateTextInputItem(onDebugTextInput, "Debug Text Input Item").Input;


            //int selectedIndex = consoleMenu.ShowMenu();
            pets = petManager.GetPets();
            PassInput(mainListener);
            running = true;
            while (running)
            {
                //clear buffers
                Renderer.ClearBuffers();
                //fetch input
                controller.Tick();
                //Renderer.FitToScreen();
                //
                foreach (IDisplayable d in displayables)
                {
                    d.Display(Renderer.PlayBuffer);
                }
                currentMenu?.Display(Renderer.MenuBuffer);
                currentOtionStrip?.Display(Renderer.OptionStripBuffer);
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
                    newPet.Name = petName;
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

        public static void onDebugItemSelected(object sender, int selectedIndex) {
            switch (selectedIndex)
            {
                case -1:
                    CloseMenu();
                    break;
                case 0:
                    if(!displayables.Contains(debugPrint))
                        displayables.Add(debugPrint);
                    else
                        displayables.Remove(debugPrint);
                    break;
                case 1:
                    PassInput(debugTextInput);
                    break;
                default: break;
            }
            
        }

        public static void onMainItemSelected(object sender, int selectedIndex) {
            switch (selectedIndex)
            {
                case -1:
                    CloseMenu();
                    break;
                case 0:
                    break;
                case 1:
                    running = false;return;
                default:
                    break;
            }
        }


        public class ChangeSizeListener : IInputListener {

            public bool IsActive { get; set; }

            OptionStrip optionStrip = new OptionStrip();

            public void KeyPressed(ConsoleKeyInfo key) {
                switch(key.Key){
                    case ConsoleKey.DownArrow:
                    {
                        

                    }break;
                    case ConsoleKey.UpArrow:
                    {

                    }
                    break;
                    case ConsoleKey.RightArrow:
                    {

                    }
                    break;
                    case ConsoleKey.LeftArrow:
                    {

                    }
                    break;
                    default: break;
                }
            }

            public OptionStrip? GetOtptionStrip() => optionStrip;
        }

        public static void onDebugTextInput(object sender, string text) {
            PopInput();
        }
        
        public class MainInputListener : IInputListener {

            public bool IsActive { get; set; }
            public void KeyPressed(ConsoleKeyInfo key) {
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        OpenMenu(mainMenu);
                        break;
                    case ConsoleKey.F2:
                        OpenMenu(debugMenu);
                        break;
                    default:
                        break;

                }
            }
        }
    }
}
