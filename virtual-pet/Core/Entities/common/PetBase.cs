using System;
using virtual_pet.Core.Models;


namespace virtual_pet.Base
{
    // Der erste Rohentwurf der Klasse
    internal abstract class PetBase
    {
        #region PROPERTYS
        public StatModel Energy = new();
        public StatModel Fed = new();
        public StatModel Hydration = new();
        #endregion PROPERTYS

        #region METH
        public void Sleep(double amount) {
            Energy.Value += amount;
        }
        public void Drink(double amount) {
            Hydration.Value += amount;
        }
        public void Eat(double amount) {
            Fed.Value += amount;
        }

        public void Tick() {
            Energy.Value -= 4.0;
            Fed.Value -= 5.0;
            Hydration.Value -= 7.0;
        }

        public string GetInfo() {
            return $"[ Energy: {Energy.Value}, Fed: {Fed.Value}, Hydration: {Hydration.Value} ]";
        }

        #endregion METH
    }
}
