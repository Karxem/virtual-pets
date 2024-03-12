using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Entity.Items;

namespace virtual_pet.Core.Util
{
    internal class ItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ItemBase);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject itemJson = JObject.Load(reader);

            // Check for the presence of the "HealingAmount" property to determine the concrete type
            if (itemJson["HealingAmount"] != null)
            {
                return itemJson.ToObject<HealingPotion>(serializer);
            }

            // Add more checks for other concrete types if needed

            throw new JsonSerializationException("Unable to determine the concrete type of the item.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
