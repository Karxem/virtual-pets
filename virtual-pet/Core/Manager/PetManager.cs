﻿using Newtonsoft.Json;
using System.Reflection;
using System.Timers;
using virtual_pet.Core.Entity.Common;

namespace virtual_pet.Core.Manager
{
    public class PetManager
    {
        private readonly string filePath;
        private static System.Timers.Timer timer;
        private static readonly int interval = 10000;
        private List<PetBase> pets;
        private readonly int NumberOfRetries = 3;
        private readonly int DelayOnRetry = 1000;

        public PetManager()
        {
            filePath = LoadFilePath();
            pets = new List<PetBase>();

            LoadPets();
            SetGameTickTimer();
        }

        public void SavePet(PetBase pet)
        {
            var existingPet = pets.Find(p => p.Name == pet.Name);

            if (existingPet == null)
            {
                pets.Add(pet);
            }
            else
            {
                UpdateExistingPet(existingPet, pet);
            }

            SavePetsToFile();
        }

        private void UpdateExistingPet(PetBase existingPet, PetBase newPet)
        {
            existingPet.Health.Value = newPet.Health;
            existingPet.Energy.Value = newPet.Energy;
            existingPet.Attack.Value = newPet.Attack;
            existingPet.Defense.Value = newPet.Defense;
            existingPet.Hunger.Value = newPet.Hunger;
            existingPet.Thirst.Value = newPet.Thirst;
            existingPet.Experience.Value = newPet.Experience;
            existingPet.Level.Value = newPet.Level;
        }

        public PetBase LoadPet(string name)
        {
            var loadedPet = pets.Find(pets => pets.Name == name);

            if (loadedPet == null)
            {
                Console.WriteLine($"Pet with name {name} not found.");
                return null;
            }

            return loadedPet;
        }

        public List<PetBase> GetPets() => pets;

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

            if (pet == null)
            {
                throw new InvalidOperationException("Failed to create a new pet instance");
            }
            
            pet.Name = name;
            pet.Type = petType;
            pet.InitPetBaseStats();
            return pet; 
        }

        public List<string> GetPetTypes()
        {
            List<string> types = new List<string>();
            foreach (Type type in Assembly.GetAssembly(typeof(PetBase)).GetTypes().Where(myType => !myType.IsAbstract && myType.IsSubclassOf(typeof(PetBase))))
            {
                types.Add(type.Name);
            }
            types.Sort();
            return types;
        }

        private string LoadFilePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string myAppFolder = Path.Combine(appDataPath, "virtual-pet");
            Directory.CreateDirectory(myAppFolder); // Create file if not exists

            string fileName = "pets.json";
            return Path.Combine(myAppFolder, fileName);
        }

        private void LoadPets()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.filePath);

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string json = reader.ReadToEnd();

                    // Deserialize directly into 'pets' list
                    pets = JsonConvert.DeserializeObject<List<PetBase>>(json, new PetConverter());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading pets from file: {ex.Message}");
            }
        }

        private void SavePetsToFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.filePath);

            string json = JsonConvert.SerializeObject(pets, Formatting.Indented);

            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath, false)) // Ensure the StreamWriter is properly disposed
                    {
                        writer.Write(json);
                        writer.Flush();
                    }
                    break;
                }
                catch (IOException e) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
            }
        }

        private void SetGameTickTimer()
        {
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var pets = GetPets();
            foreach (var pet in pets)
            {
                if (pet == null)
                    continue;

                var petInstance = LoadPet(pet.Name);
                if (petInstance != null)
                {
                    petInstance.GameTick();

                    SavePet(petInstance);
                }
            }
        }
    }
}
