using Newtonsoft.Json;
using virtual_pet.Core.Entities.Items;
using virtual_pet.Core.Utils;

namespace virtual_pet.Core.Manager
{
    internal class InventoryManager
    {
        private readonly string filePath;
        private List<ItemBase> items;

        public InventoryManager()
        {
            filePath = LoadFilePath();

            items = new List<ItemBase>();
            LoadItems();
        }

        public void SaveItem(ItemBase item)
        {
            var existingItem = items.Find(p => p.Name == item.Name);

            if (existingItem == null)
            {
                items.Add(item);
            }
            else
            {
                existingItem.AddItem(1);
            }

            SaveItemsToFile();
        }

        public ItemBase LoadItem(string name)
        {
            var loadedItem = items.Find(items => items.Name == name);

            if (loadedItem == null)
            {
                Console.WriteLine($"Item with name {name} not found.");
                return null;
            }

            return loadedItem;
        }

        public List<ItemBase> GetItems() => items;

        private string LoadFilePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string myAppFolder = Path.Combine(appDataPath, "virtual-pet");
            Directory.CreateDirectory(myAppFolder); // Create file if not exists

            string fileName = "inventory.json";
            return Path.Combine(myAppFolder, fileName);
        }

        private void LoadItems()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.filePath);

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string json = reader.ReadToEnd();

                    var settings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> { new ItemConverter() },
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    // Deserialize directly into 'items' list
                    items = JsonConvert.DeserializeObject<List<ItemBase>>(json, settings);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading items from file: {ex.Message}");
            }
        }

        private void SaveItemsToFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.filePath);

            string json = JsonConvert.SerializeObject(items, Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(filePath, false)) // Ensure the StreamWriter is properly disposed
            {
                writer.Write(json);
                writer.Flush();
            }
        }

    }
}
