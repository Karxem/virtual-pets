using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Entities.Common {
    public struct TileInfo {
        public const byte FLAG_COLLIDABLE = 0x01;

        char character;
        ConsoleColor background;
        ConsoleColor foreground;
        byte flags;
    }
}
