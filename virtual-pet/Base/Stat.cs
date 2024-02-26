using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Base
{
    //ToDo: Leveling einbinden
    internal class Stat
    {
        private double _Value = 0.0;
        private double _MinValue = 0.0;
        private double _MaxValue = 0.0;

        //ToDo: Leveling Auskapseln
        private int _Level = 1;
        private int _MaxLevel = 10;
        private double _LevelMaxIncrease = 2.5;

        public double Value
        {
            get { return _Value; }
            set { _Value = Math.Min(100, Math.Max(0, value)); }
        }

        public static implicit operator double(Stat stat ) { return stat.Value; }
        public static double operator +(Stat stat, double value) {
            return stat.Value + value;
        }
        public static double operator -(Stat stat, double value) {
            return stat.Value - value;
        }

    }
}
