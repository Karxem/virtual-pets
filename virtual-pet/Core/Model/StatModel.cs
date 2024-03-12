namespace virtual_pet.Core.Model
{
    public class StatModel
    {
        private double _value;
        private double _minValue;
        private double _maxValue;

        public StatModel(double minValue, double maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _value = Math.Min(_maxValue, Math.Max(_minValue, 0.0));
        }

        public double Value
        {
            get => _value;
            set => _value = Math.Min(_maxValue, Math.Max(_minValue, value));
        }

        public double MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }

        public double MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public void SetRange(double minValue, double maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            Value = _value;
        }

        public static implicit operator double(StatModel stat) => stat.Value;

        public static double operator +(StatModel stat, double value) => stat.Value + value;

        public static double operator -(StatModel stat, double value) => stat.Value - value;
    }
}
