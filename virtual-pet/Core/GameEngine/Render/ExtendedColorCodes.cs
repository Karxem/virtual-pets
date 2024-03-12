using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Engine.Render
{
    public class ExtendedColorCodes
    {
        public static string GetBackground(int color)
        {
            if (color < 0 || color > 255)
                return "";
            return $"\u00b1[48;5;{color}m";

        }
        public static string GetForeground(int color)
        {
            if (color < 0 || color > 255)
                return "";
            return $"\u00b1[38;5;{color}m";

        }

        public static string GetColor(int bg, int fg)
        {
            if (bg < 0 || bg > 255 || fg < 0 || fg > 255)
                return "";

            return $"\u00b1[38;5;{fg};48;5;{bg}m";

        }
    }
}
