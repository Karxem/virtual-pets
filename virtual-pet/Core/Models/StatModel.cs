using System;

namespace virtual_pet.Core.Models
{
    internal class StatModel
    {
        private double _Value;
        private double _MinValue;
        private double _MaxValue;

        public StatModel(double minValue, double maxValue)
        {
            _MinValue = minValue;
            _MaxValue = maxValue;
            _Value = Math.Min(_MaxValue, Math.Max(_MinValue, 0.0));
        }

        public double Value
        {
            get { return _Value; }
            set { _Value = Math.Min(_MaxValue, Math.Max(_MinValue, value)); }
        }

        public double MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; }
        }

        public double MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }

        public void SetRange(double minValue, double maxValue)
        {
            _MinValue = minValue;
            _MaxValue = maxValue;
            Value = _Value;
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
