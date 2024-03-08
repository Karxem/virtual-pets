using Newtonsoft.Json;
using System.Reflection;
using System.Timers;
using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Entities.Pets;
using virtual_pet.Core.Models;

namespace virtual_pet.Core.Managers
{
    internal class PetManager
    {
        private readonly string filePath;
        private static System.Timers.Timer timer;
        private static readonly int interval = 10000;
        private List<PetModel> petModels = new List<PetModel>();

        public PetManager()
        {
            filePath = LoadFilePath();

            new Lenora();
            new Flololo();
            LoadPets();
            SetGameTickTimer();
        }

        private string LoadFilePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string myAppFolder = Path.Combine(appDataPath, "virtual-pet");
            Directory.CreateDirectory(myAppFolder); // Create file if not exists

            string fileName = "pets.json";
            return Path.Combine(myAppFolder, fileName);
        }

        public void SavePet(PetBase pet)
        {
            PetModel petModel = new PetModel(
                name: pet.PetName,
                type: pet.GetPetType(),
                health: pet.Health.Value,
                energy: pet.Energy.Value,
                attack: pet.Attack.Value,
                defense: pet.Defense.Value,
                hunger: pet.Hunger.Value,
                thirst: pet.Thirst.Value,
                exp: pet.Experience.Value,
                level: pet.Level.Value
            );

            // Check if there is an existing pet with the same name
            var existingPet = petModels.Find(p => p.Name == petModel.Name);

            if (existingPet == null)
            {
                petModels.Add(petModel);
            }
            else
            {
                UpdateExistingPet(existingPet, petModel);
            }

            SavePetsToFile();
        }

        private void UpdateExistingPet(PetModel existingPet, PetModel newPet)
        {
            existingPet.Health = newPet.Health;
            existingPet.Energy = newPet.Energy;
            existingPet.Attack = newPet.Attack;
            existingPet.Defense = newPet.Defense;
            existingPet.Hunger = newPet.Hunger;
            existingPet.Thirst = newPet.Thirst;
            existingPet.Experience = newPet.Experience;
            existingPet.Level = newPet.Level;
        }

        // Load a pet from file based on its name
        public PetBase LoadPet(string name)
        {
            var loadedPetModel = petModels.Find(p => p.Name == name);

            if (loadedPetModel == null)
            {
                Console.WriteLine($"Pet with name {name} not found.");
                return null;
            }

            // Create a new PetBase instance and set its state based on the loaded PetModel
            PetBase pet = CreateNewPetInstance(loadedPetModel.Name, loadedPetModel.Type);

            pet.Health.Value = loadedPetModel.Health;
            pet.Energy.Value = loadedPetModel.Energy;
            pet.Attack.Value = loadedPetModel.Attack;
            pet.Defense.Value = loadedPetModel.Defense;
            pet.Hunger.Value = loadedPetModel.Hunger;
            pet.Thirst.Value = loadedPetModel.Thirst;
            pet.Experience.Value = loadedPetModel.Experience;
            pet.Level.Value = loadedPetModel.Level;

            return pet;
        }

        public List<PetModel> GetPets() => petModels;

        public PetBase CreateNewPetInstance(string name, string petType)
        {
            if (petType == null)
            {
                Console.WriteLine("Invalid pet type");
                return null;
            }

            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => typeof(PetBase).IsAssignableFrom(t) && t.Name == petType);

            if (type == null)
            {
                Console.WriteLine($"Pet type '{petType}' not found or not derived from PetBase.");
                return null;
            }

            PetBase pet = (PetBase)Activator.CreateInstance(type);

            if (pet != null)
            {
                pet.PetName = name;
                pet.InitPetBaseStats();
                return pet;
            }

            throw new InvalidOperationException("Failed to create a new pet instance");
        }

        private void LoadPets()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.filePath);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }

            string json = File.ReadAllText(filePath);
            petModels = JsonConvert.DeserializeObject<List<PetModel>>(json);
        }

        private void SavePetsToFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.filePath);

            string json = JsonConvert.SerializeObject(petModels, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void SetGameTickTimer()
        {
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var pets = GetPets();
            foreach (var pet in pets)
            {
                if (pet == null)
                    continue;

                var petInstance = LoadPet(pet.Name);
                petInstance?.Tick();

                SavePet(petInstance);
            }
        }
    }
}
