using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{

    /*
     * The coloring has to be checked on Linux (Mac)
     * 
     */
    public class ColorCodes
    {
  

        private ColorCodes()
        {
        }


        public static string GetColor(ConsoleColor Backgrground, ConsoleColor Foreground)
        {
            string col = "\u001b[";

            switch (Foreground)
            {
                case ConsoleColor.Black:        col+= "30"; break;
                case ConsoleColor.DarkRed:      col+= "31"; break;
                case ConsoleColor.DarkGreen:    col+= "32"; break;
                case ConsoleColor.DarkYellow:   col+= "33"; break;
                case ConsoleColor.DarkBlue:     col+= "34"; break;
                case ConsoleColor.DarkMagenta:  col+= "35"; break;
                case ConsoleColor.DarkCyan:     col+= "36"; break;
                case ConsoleColor.Gray:         col+= "37"; break;

                case ConsoleColor.DarkGray:     col+= "90"; break;
                case ConsoleColor.Red:          col+= "91"; break;
                case ConsoleColor.Green:        col+= "92"; break;
                case ConsoleColor.Yellow:       col+= "93"; break;
                case ConsoleColor.Blue:         col+= "94"; break;
                case ConsoleColor.Magenta:      col+= "95"; break;
                case ConsoleColor.Cyan:         col+= "96"; break;
                case ConsoleColor.White:        col+= "97"; break;
                default: return GetBackground(Backgrground);
            }
            switch (Backgrground)
            {
                case ConsoleColor.Black:        col+= ";40m";  break;
                case ConsoleColor.DarkRed:      col+= ";41m";  break;
                case ConsoleColor.DarkGreen:    col+= ";42m";  break;
                case ConsoleColor.DarkYellow:   col+= ";43m";  break;
                case ConsoleColor.DarkBlue:     col+= ";44m";  break;
                case ConsoleColor.DarkMagenta:  col+= ";45m";  break;
                case ConsoleColor.DarkCyan:     col+= ";46m";  break;
                case ConsoleColor.Gray:         col+= ";47m";  break;

                case ConsoleColor.DarkGray:     col+= ";100m"; break;
                case ConsoleColor.Red:          col+= ";101m"; break;
                case ConsoleColor.Green:        col+= ";102m"; break;
                case ConsoleColor.Yellow:       col+= ";103m"; break;
                case ConsoleColor.Blue:         col+= ";104m"; break;
                case ConsoleColor.Magenta:      col+= ";105m"; break;
                case ConsoleColor.Cyan:         col+= ";106m"; break;
                case ConsoleColor.White:        col+= ";107m"; break;
                default: return GetForeground(Foreground);
            }
            return col;

        }

        public static string GetForeground(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black:        return "\u001b[30m";
                case ConsoleColor.DarkRed:      return "\u001b[31m";
                case ConsoleColor.DarkGreen:    return "\u001b[32m";
                case ConsoleColor.DarkYellow:   return "\u001b[33m";
                case ConsoleColor.DarkBlue:     return "\u001b[34m";
                case ConsoleColor.DarkMagenta:  return "\u001b[35m";
                case ConsoleColor.DarkCyan:     return "\u001b[36m";
                case ConsoleColor.Gray:         return "\u001b[37m";

                case ConsoleColor.DarkGray:     return "\u001b[90m";
                case ConsoleColor.Red:          return "\u001b[91m";
                case ConsoleColor.Green:        return "\u001b[92m";
                case ConsoleColor.Yellow:       return "\u001b[93m";
                case ConsoleColor.Blue:         return "\u001b[94m";
                case ConsoleColor.Magenta:      return "\u001b[95m";
                case ConsoleColor.Cyan:         return "\u001b[96m";
                case ConsoleColor.White:        return "\u001b[97m";
                default: return "";
            }


        }

        public static string GetBackground(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black:        return "\u001b[40m";
                case ConsoleColor.DarkRed:      return "\u001b[41m";
                case ConsoleColor.DarkGreen:    return "\u001b[42m";
                case ConsoleColor.DarkYellow:   return "\u001b[43m";
                case ConsoleColor.DarkBlue:     return "\u001b[44m";
                case ConsoleColor.DarkMagenta:  return "\u001b[45m";
                case ConsoleColor.DarkCyan:     return "\u001b[46m";
                case ConsoleColor.Gray:         return "\u001b[47m";
                                                        
                case ConsoleColor.DarkGray:     return "\u001b[100m";
                case ConsoleColor.Red:          return "\u001b[101m";
                case ConsoleColor.Green:        return "\u001b[102m";
                case ConsoleColor.Yellow:       return "\u001b[103m";
                case ConsoleColor.Blue:         return "\u001b[104m";
                case ConsoleColor.Magenta:      return "\u001b[105m";
                case ConsoleColor.Cyan:         return "\u001b[106m";
                case ConsoleColor.White:        return "\u001b[107m";
                default: return "";
            }

        }   
    }
}
