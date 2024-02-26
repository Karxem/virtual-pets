using virtual_pet.Core.Models;


namespace virtual_pet.Core.Entities.Common
{
    internal abstract class PetBase
    {
        #region PROPERTYS
        public string Name {  get; set; }
        public StatModel Health = new();
        public StatModel Energy = new();
        public StatModel Hunger = new();
        public StatModel Thirst = new();
        #endregion PROPERTYS

        #region METH
        public void Heal(double amount)
        {
            Health.Value += amount;
        }
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

        public void FillAll()
        {
            Health.Value += 100;
            Energy.Value += 100;
            Thirst.Value += 100;
            Hunger.Value += 100;
        }

        // TODO: We should add dynamic handling here with a random value inside a range, since hard coded values are meh
        public void Tick()
        {
            Health.Value -= 2.0;
            Energy.Value -= 4.0;
            Hunger.Value -= 5.0;
            Thirst.Value -= 7.0;
        }

        public string GetInfo()
        {
            return $"[ Health: {Health.Value},Energy: {Energy.Value}, Hunger: {Hunger.Value}, Thirst: {Thirst.Value} ]";
        }

        // Abstract method to get the pet type
        public abstract string GetPetType();
        #endregion METH
    }
}
