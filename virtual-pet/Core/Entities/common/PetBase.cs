using virtual_pet.Core.Models;


namespace virtual_pet.Core.Entities.Common
{
    internal abstract class PetBase
    {
        #region PROPERTYS
        public string Name {  get; set; }
        public Stat Energy = new();
        public Stat Hunger = new();
        public Stat Thirst = new();
        #endregion PROPERTYS

        #region METH
        public void Sleep(double amount)
        {
            Energy.Value += amount;
        }
        public void Drink(double amount)
        {
            Thirst.Value += amount;
        }
        public void Eat(double amount)
        {
            Hunger.Value += amount;
        }

        // TODO: We should add dynamic handling here with a random value inside a range, since hard coded values are meh
        public void Tick()
        {
            Energy.Value -= 4.0;
            Hunger.Value -= 5.0;
            Thirst.Value -= 7.0;
        }

        public string GetInfo()
        {
            return $"[ Energy: {Energy.Value}, Hunger: {Hunger.Value}, Thirst: {Thirst.Value} ]";
        }

        // Abstract method to get the pet type
        public abstract string GetPetType();
        #endregion METH
    }
}
