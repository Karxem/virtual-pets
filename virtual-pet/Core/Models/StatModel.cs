using System.Numerics;

namespace virtual_pet.Core.Models {
    // TODO: Add a leveling handler to enhance stats over time
    // TODO: Remove abstract methods, we need to keep track of functions inside Base classes and models
    internal class StatModel
    {
        private double _Value = 0.0;
        private double _MinValue = 0.0;
        private double _MaxValue = 0.0;

        private int _Level = 0;
        private int _MaxLevel = 10;
        private int _LevelUpThreshhold = 200;

        // We should make this abstract maybe
        private double[] _LevelMaxIncrese =
        { 0, 2.5, 8.0,  15.0, 20.0, 25.0, 50.0, 70.0, 90.0, 100.0 };

        public double Value
        {
            get { return _Value; }
            set { _Value = Math.Min(100, Math.Max(0, value)); }
        }

        public static implicit operator double(StatModel stat) { return stat.Value; }

        public static double operator +(StatModel stat, double value)
        {
            return stat.Value + value;
        }

        public static double operator -(StatModel stat, double value)
        {
            return stat.Value - value;
        }

    }
}
