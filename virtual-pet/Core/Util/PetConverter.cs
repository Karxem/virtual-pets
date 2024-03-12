using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using virtual_pet.Core.Entity.Common;

internal class PetConverter : JsonConverter<PetBase>
{
    public override PetBase ReadJson(JsonReader reader, Type objectType, PetBase existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);

        string typeName = jsonObject["Type"]?.ToString();

        if (typeName == null)
        {
            Console.WriteLine("Pet type is missing in JSON.");
            return null;
        }

        // Use reflection to create an instance of the concrete type based on the "Type" property
        Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == typeName);

        if (type == null || !typeof(PetBase).IsAssignableFrom(type))
        {
            Console.WriteLine($"Invalid or unknown type: {typeName}");
            return null;
        }

        PetBase pet = (PetBase)Activator.CreateInstance(type);
        serializer.Populate(jsonObject.CreateReader(), pet);
        return pet;
    }

    public override void WriteJson(JsonWriter writer, PetBase value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
