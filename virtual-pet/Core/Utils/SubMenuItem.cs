using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Utils {
    public class SubMenuItem : MenuItemBase{

        
        public ConsoleMenu Menu { get; private set; }

        public SubMenuItem(int id, string text ) : base(id, text) { }

    }
}
