using Newtonsoft.Json;
using System.Timers;
using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Models;


namespace virtual_pet.Core.Managers
{
    internal class PetManager
    {
        private string FilePath;
        private System.Timers.Timer Timer;
        private static int Interval = 10000;
        private List<PetModel> petModels;

        public PetManager()
        {
            FilePath = LoadFilePath();
            LoadPets();
            SetTimer();
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
                hunger: pet.Hunger.Value,
                thirst: pet.Thirst.Value
            );

            // Check if there is an existing pet with the same name
            var existingPet = petModels.Find(p => p.Name == petModel.Name);

            if (existingPet == null)
            {
                petModels.Add(petModel);
            }
            else
            {
                existingPet.Health = petModel.Health;
                existingPet.Energy = petModel.Energy;
                existingPet.Hunger = petModel.Hunger;
                existingPet.Thirst = petModel.Thirst;
            }

            SavePetsToFile();
        }

        // Load a pet from file based on its name
        public PetBase LoadPet(string name)
        {
            var loadedPetModel = petModels.Find(p => p.Name == name);

            if (loadedPetModel == null)
            {
                return null;
            }

            // Create a new PetBase instance and set its state based on the loaded PetModel
            PetBase pet = CreatePetInstance(loadedPetModel.Type);
            pet.PetName = loadedPetModel.Name;
            pet.Health.Value = loadedPetModel.Health;
            pet.Energy.Value = loadedPetModel.Energy;
            pet.Hunger.Value = loadedPetModel.Hunger;
            pet.Thirst.Value = loadedPetModel.Thirst;

            return pet;
        }

        public List<PetModel> GetPets()
        {
            return petModels;
        }

        private void LoadPets()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }

            string json = File.ReadAllText(filePath);
            petModels = JsonConvert.DeserializeObject<List<PetModel>>(json);
        }

        private void SavePetsToFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

            string json = JsonConvert.SerializeObject(petModels, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void SetTimer()
        {
            Timer = new System.Timers.Timer(Interval);
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var pets = GetPets();
            foreach (var pet in  pets)
            {
                if (pet == null) {
                    return;
                }

                var petInstance = LoadPet(pet.Name);
                petInstance.Tick();

                SavePet(petInstance);
            }
        }

        private static PetBase CreatePetInstance(string type)
        {
            switch (type)
            {
                case "Lenora":
                    return new Lenora();
                default:
                    throw new ArgumentException("Invalid pet type");
            }
        }
    }
}
